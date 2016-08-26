delimiter |
CREATE EVENT `abrirYcerrarCaja` 
ON SCHEDULE EVERY 1 DAY STARTS '2016-08-26 23:00:00'
DO BEGIN

SELECT @totalCaja := montoInicial FROM caja 
WHERE Fecha= date(now());

SELECT @totalEntrada := sum(Monto)
FROM movimientoscaja M 
INNER JOIN tipoconcepcio C on m.IdConcep = c.idConcepto
WHERE M.Fecha=date(now())
AND C.SalidaEntrada= "Ingreso";

SELECT @totalSalida := sum(Monto)
FROM movimientoscaja M 
INNER JOIN tipoconcepcio C on m.IdConcep = c.idConcepto
WHERE M.Fecha=date(now())
AND C.SalidaEntrada= "Salida";


SET @totalCaja = @totalCaja + @totalEntrada - @totalSalida;

UPDATE caja set montoFinal= @totalCaja
where Fecha= date(now());

INSERT caja (Fecha,montoInicial) VALUES(CURDATE() + INTERVAL 1 DAY, @totalCaja);
END |
delimiter ;




