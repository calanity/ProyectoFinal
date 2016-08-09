/*call   TotalSalidasPorMes("2016","07")*/
SELECT SUM(Monto) AS Monto 
FROM movimientos m INNER JOIN tipoconcepcio c
ON m.IdConcepto = c.IdConcepto
WHERE MONTH(Fecha)= mes AND YEAR(Fecha)=anio and SalidaEntrada= "Salida"


Select *
 from tipoconcepcio 