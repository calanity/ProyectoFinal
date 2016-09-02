delimiter |
CREATE EVENT `abrirYcerrarCaja` 
ON SCHEDULE EVERY 1 DAY STARTS '2016-09-2 13:36:00'
DO BEGIN

SELECT @totalCaja := montoInicial
 FROM caja 
WHERE Fecha= date(now());

SELECT @totalEntrada := ifnull(sum(Monto),0)
FROM movimientoscaja M 
INNER JOIN tipoconcepcio C on m.IdConcep = c.idConcepto
WHERE M.Fecha=date(now())
AND C.SalidaEntrada= "Ingreso";

SELECT @totalSalida := ifnull(sum(Monto),0)
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

select now()


SET GLOBAL event_scheduler = ON;

 GRANT SUPER ON *.* TO user@'bcaf7709a9bf09' IDENTIFIED BY 'password';
 FLUSH PRIVILEGES;
