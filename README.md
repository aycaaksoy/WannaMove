# WannaMove

<p>  1) asp.net core 5in visual studioda kurulu olduğundan emin ol / Mssql indir <br/> 2) clone repository in visual studio <br/> 3) appsettings.json dosyasındaki default connection kısmına kendi sql server, user , id bilgilerini gir<br/> 4) Tools kısmından -> NuGet Package Manager -> Package Manager Console'a tıkla
<br/> 5) gelen konsola ->' add-migration "setup" 'yaz ve enter 6)migration dosyası doğru yaratılınca konsola update-database yaz enterla </br> 7)Mssql'i aç database'in gelmiş olması lazım <br/> 
8) bu query'i mssqlde çalıştır (csv dosyasının yolunun doğru olduğundan emin ol! ve projenin içinde bulunan csv dosyasını kullan ) vee datamız table'a eklenmiş olmalı </br> </br>   SQL QUERY: </br> </br> <b> BULK INSERT dbo.UaScoresDataFrame

FROM 'C:\Users\aycaa\source\repos\WannaMove\WannaMove\wwwroot\uaScoresDataFrame.csv'

WITH
(
        FORMAT='CSV',
        FIRSTROW=2
)

GO </b> </p>
