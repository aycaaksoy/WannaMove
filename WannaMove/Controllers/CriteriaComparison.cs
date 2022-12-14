using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WannaMove.Data;

namespace WannaMove.Controllers
{
    public class CriteriaComparison : Controller
    {
        private readonly ApplicationDbContext _context;

        public CriteriaComparison(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult PairwiseComparison()
        {
            return View();
        }

       
        public IActionResult PairwiseComparisonError()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Result(Dictionary<string, double> response)
        {

            return View(response);
        }

        [HttpPost]
        public JsonResult CalculateAhp2(string[] SelectedCriteria, string[] filteredContinents, string[] Ratings)
        {
            string[] selectedCriteria = SelectedCriteria;
            string[] filterContinents = filteredContinents;
            string[] ratings = Ratings;
            Dictionary<string, double> res = new Dictionary<string, double>();
            res = CalculateAhp(selectedCriteria, filterContinents, ratings);

            if (res == null)
            {
                return Json(null);
            }
            else
            {
                return Json(res);
            }
            
        }

        public Dictionary<string, double> CalculateAhp(string[] kriterler, string[] kitalar, string[] reytingler)
        {
            //model ekledik?
            var c = _context.UaScoresDataFrame.ToList();
            // Manipulate the arrays here

            string[] selectedCriteria = kriterler;
            string[] filterContinents = kitalar;
            string[] ratings = reytingler;
            //0-1, 0-2 , 0-3 , 1-2 , 1-3 , 2-3
            double[] doubleRatings = Array.ConvertAll(ratings, double.Parse);

            for (int i = 0; i < doubleRatings.Length; i++)
            {
                if (doubleRatings[i] < 0)
                {
                    doubleRatings[i] = -1 / (doubleRatings[i] - 1);
                }
                else if (doubleRatings[i] == 0)
                {
                    doubleRatings[i] = 1;
                }
                else if (doubleRatings[i] > 0)
                {
                    doubleRatings[i] = doubleRatings[i] + 1;
                }
            }

            Dictionary<string, Dictionary<string, double>> pairwiseComparisonScores = new Dictionary<string, Dictionary<string, double>>()
            {
                {selectedCriteria[0] , new Dictionary<string, double> {[selectedCriteria[0]] = 1, [selectedCriteria[1]] = doubleRatings[0], [selectedCriteria[2]] = doubleRatings[1], [selectedCriteria[3]] = doubleRatings[2]}},
                {selectedCriteria[1] , new Dictionary<string, double> {[selectedCriteria[0]] = 1/doubleRatings[0], [selectedCriteria[1]] = 1, [selectedCriteria[2]] = doubleRatings[3], [selectedCriteria[3]] = doubleRatings[4]}},
                {selectedCriteria[2] , new Dictionary<string, double> {[selectedCriteria[0]] = 1/doubleRatings[1], [selectedCriteria[1]] = 1/doubleRatings[3], [selectedCriteria[2]] = 1, [selectedCriteria[3]] = doubleRatings[5]}},
                {selectedCriteria[3] , new Dictionary<string, double> {[selectedCriteria[0]] = 1/doubleRatings[2], [selectedCriteria[1]] = 1/doubleRatings[4], [selectedCriteria[2]] = 1/doubleRatings[5], [selectedCriteria[3]] = 1}}
            };


            //Declaring variables
            List<string> criteriaList = new List<string>()
            {
                selectedCriteria[0],
                selectedCriteria[1],
                selectedCriteria[2],
                selectedCriteria[3]
            };

            // Create a dictionary to store the results, with city names as keys and dictionaries as values
            Dictionary<string, Dictionary<string, double>> filteredCityScores = new Dictionary<string, Dictionary<string, double>>();

            // Connect to the database
            SqlConnection connection = new SqlConnection("Server=DESKTOP-60DI5A8;Database=DbWannaMove;User Id=aycaa; Password=admin; integrated security=True;");
            connection.Open();

            //SqlCommand command = new SqlCommand();

            // Create a command to select the cities from the database
            
            
            SqlCommand command = new SqlCommand("SELECT * FROM UaScoresDataFrame", connection);
            
            if (filterContinents[0] != null)
            {
                 //command = new SqlCommand("SELECT * FROM UaScoresDataFrame WHERE Continent IN (@continents)", connection);
                string sql1 = "SELECT * FROM UaScoresDataFrame WHERE " ;
                string sql2 = "";

                for (int i = 0; i < filterContinents.Length; i++)
                {

                    if (i > 0)
                    {
                        sql2 += " OR ";
                    }

                    sql2 += "Continent='" + filterContinents[i] + "'";
                }

                command = new SqlCommand(sql1 + sql2, connection);

                
            }

           

            // Execute the command and read the results
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Get the city name and store it in a variable
                string cityName = reader["CityName"].ToString();

                // Create a dictionary to store the scores for the city
                Dictionary<string, double> cityScores = new Dictionary<string, double>();

                // Loop through the selected criteria and get the scores for each one
                foreach (string criteria in selectedCriteria)
                {
                    double score = (double)reader[criteria];
                    cityScores.Add(criteria, score);
                }

                // Add the city name and scores to the results dictionary
                filteredCityScores.Add(cityName, cityScores);
            }

            // Close the reader and connection
            reader.Close();
            connection.Close();


            Dictionary<string, double> topCities =null;

            // Check if pairwise comparison is consistent
            bool isConsistent = IsPairwiseComparisonConsistent(createPWComparisonMatrix(pairwiseComparisonScores));
            if (!isConsistent)
            {
                return topCities;
            }
            else
            {
                //Declaring Priority Vector for calculating AHP scores of cities
                double[,] PWComparisonMatrix = createPWComparisonMatrix(pairwiseComparisonScores);
                double[] priorityVector = CalculateWeights(PWComparisonMatrix);

                Dictionary<string, double> criteriaWeights = new Dictionary<string, double>()
                {
                    {criteriaList[0], priorityVector[0]},
                    {criteriaList[1], priorityVector[1]},
                    {criteriaList[2], priorityVector[2]},
                    {criteriaList[3], priorityVector[3]}
                };

                // Calculate AHP scores for cities
                Dictionary<string, double> cityAhpScores = CalculateCityAhpScores(criteriaWeights, filteredCityScores);

                // Select top 3 cities
                topCities = SelectTopCities(cityAhpScores);

                return topCities;
            }



        }
        //
        static double[,] createPWComparisonMatrix(Dictionary<string, Dictionary<string, double>> pairwiseComparisonScores)
        {

            int n = pairwiseComparisonScores.Count;
            double[,] PWComparisonMatrix = new double[n, n];
            int i = 0;
            int j = 0;
            foreach (string criteria1 in pairwiseComparisonScores.Keys)
            {
                j = 0;
                foreach (string criteria2 in pairwiseComparisonScores[criteria1].Keys)
                {
                    PWComparisonMatrix[i, j] = pairwiseComparisonScores[criteria1][criteria2];
                    j++;
                }
                i++;
            }


            /*//for printing the matrix
            for (int a = 0; a < PWComparisonMatrix.GetLength(0); a++)
            {
                for (int b = 0; b < PWComparisonMatrix.GetLength(1); b++)
                {
                    Console.Write(PWComparisonMatrix[a, b] + "\t");
                }
                Console.WriteLine();
            }*/

            return PWComparisonMatrix;
        }
        //Returns pair-wise comparison matrix as a two dimensional double array.

        static bool IsPairwiseComparisonConsistent(double[,] PWComparisonMatrix)
        {
            // Calculate consistency index (CI) and consistency ratio (CR)               
            double[] weights = CalculateWeights(PWComparisonMatrix);
            double ci = CalculateConsistencyIndex(PWComparisonMatrix, weights);
            double cr = ci / 0.9;

            // Check if CR is less than 0.1 (consistent) or greater than 0.1 (not consistent)
            if (cr < 0.1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Checks Consistency Ratio, returns true if consistent, false if inconsistent.

        static double[] CalculateWeights(double[,] PWComparisonMatrix)
        {
            int n = PWComparisonMatrix.GetLength(0);

            double[] columnTotals = new double[n];

            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += PWComparisonMatrix[j, i];
                }
                columnTotals[i] = sum;
            }

            double[,] normalisedMatrix = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    normalisedMatrix[j, i] = PWComparisonMatrix[j, i] / columnTotals[i];
                }
            }

            double[] weights = new double[n];

            for (int i = 0; i < n; i++)
            {
                double rowSum = 0.0;

                for (int j = 0; j < n; j++)
                {
                    rowSum += normalisedMatrix[i, j];
                }

                weights[i] = rowSum;
            }

            for (int i = 0; i < n; i++)
            {
                weights[i] = weights[i] / n;
            }
            /*//for printing the vector
            for (int a = 0; a < weights.GetLength(0); a++)
            {

                Console.Write(weights[a] + "\t");

            }*/

            return weights;
        }
        //Returns Priority Vector as an Array

        static double CalculateConsistencyIndex(double[,] matrix, double[] weights)
        {
            int n = matrix.GetLength(0);

            double[,] weightedScores = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    weightedScores[j, i] = matrix[j, i] * weights[i];
                }
            }

            double[] weightedScoreTotals = new double[n];
            for (int i = 0; i < n; i++)
            {
                double rowTotal = 0.0;
                for (int j = 0; j < n; j++)
                {
                    rowTotal += weightedScores[i, j];
                }
                weightedScoreTotals[i] = rowTotal;
            }

            double[] lambdas = new double[n];
            for (int i = 0; i < n; i++)
            {
                lambdas[i] = weightedScoreTotals[i] / weights[i];
            }

            double lambdaMax = lambdas.Sum() / n;

            var consistencyIndex = (lambdaMax - n) / (n - 1);
            return consistencyIndex;
        }
        //Returns Consistency Index


        static Dictionary<string, double> CalculateCityAhpScores(Dictionary<string, double> criteriaWeights, Dictionary<string, Dictionary<string, double>> CityScore)
        {
            // Initialize cityAhpScores dictionary
            Dictionary<string, double> cityAhpScores = new Dictionary<string, double>();

            foreach (string cityName in CityScore.Keys)
            {
                double ahpScore = 0;

                foreach (string criteria in criteriaWeights.Keys)
                {
                    ahpScore += criteriaWeights[criteria] * CityScore[cityName][criteria];
                }
                cityAhpScores[cityName] = ahpScore;
            }

            return cityAhpScores;
        }
        // Returns a Dictionary with city(String) as Key, calculated AHP score(double) as value.


        static Dictionary<string, double> SelectTopCities(Dictionary<string, double> cityAhpScores)
        {
            // Sort cityAhpScores dictionary by values in descending order
            //var sortedCityAhpScores = from entry in cityAhpScores orderby entry.Value descending select entry;
            //Dictionary<string, double> sortedCityAhpScores = (from entry in cityAhpScores orderby entry.Value descending select entry);
            //cityAhpScores.Count()
            var sortedCityAhpScores = cityAhpScores.OrderByDescending(pair => pair.Value).Take(10)
               .ToDictionary(pair => pair.Key, pair => pair.Value);

            // Select top 3 cities
            //List<string> topCities = new List<string>();
            //int i = 0;
            //foreach (KeyValuePair<string, double> entry in sortedCityAhpScores)
            //{
            //    if (i == 3)
            //    {
            //        break;
            //    }
            //    topCities.Add(entry.Key);
            //    i++;
            //}


            return sortedCityAhpScores;
        }
    }

}
