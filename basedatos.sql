-- phpMyAdmin SQL Dump
-- version 4.5.2
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 05-08-2016 a las 11:42:35
-- Versión del servidor: 5.7.9
-- Versión de PHP: 5.6.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `basedatos`
--

DELIMITER $$
--
-- Procedimientos
--
DROP PROCEDURE IF EXISTS `ActualizarArticulo`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ActualizarArticulo` (IN `Nomb` TEXT, IN `cate` INT, IN `prec` INT, IN `stkActual` INT, IN `stkMinimo` INT, IN `idArti` INT)  NO SQL
UPDATE articulos SET Nombre= Nomb, Idcat =cate, Precio= prec, StockActual= stkActual, StockMinimo = stkMinimo 
WHERE idArticulos = idArti$$

DROP PROCEDURE IF EXISTS `ActualizarProductoDV`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ActualizarProductoDV` (IN `canti` INT, IN `subti` INT, IN `idVen` INT, IN `idArtic` INT)  NO SQL
UPDATE detalleventa SET cantidad = canti , subtotal = subti
WHERE idVenta = idVen AND idArticulo= idArtic$$

DROP PROCEDURE IF EXISTS `actualizarVenta`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `actualizarVenta` (IN `tot` INT, IN `idVen` INT, IN `mpago` TEXT)  NO SQL
UPDATE ventas SET MontoTotal = tot , MedioPago = mpago
WHERE idVentas = idVen$$

DROP PROCEDURE IF EXISTS `BuscoCategoriasActivas`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `BuscoCategoriasActivas` ()  NO SQL
SELECT * FROM categorias where eliminada= false$$

DROP PROCEDURE IF EXISTS `BuscoPorductosActivos`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `BuscoPorductosActivos` ()  NO SQL
SELECT * FROM articulos where eliminado=false$$

DROP PROCEDURE IF EXISTS `crearArticulo`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `crearArticulo` (IN `Nomb` TEXT, IN `cate` TEXT, IN `prec` INT, IN `stkActual` INT, IN `stkMinimo` INT)  INSERT INTO articulos (Nombre, Idcat, Precio, StockActual, StockMinimo)  VALUES ( Nomb, cate, prec, stkActual, stkMinimo)$$

DROP PROCEDURE IF EXISTS `CrearCategoria`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `CrearCategoria` (IN `nomb` TEXT)  NO SQL
INSERT INTO categorias(Nombre) VALUES(nomb)$$

DROP PROCEDURE IF EXISTS `crearDetalleVenta`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `crearDetalleVenta` (IN `idArti` INT, IN `prec` INT, IN `canti` INT, IN `subt` INT, IN `idVen` INT)  NO SQL
INSERT INTO detalleventa (idArticulo , precio, cantidad, subtotal, idVenta) VALUES (idArti, prec, canti, subt, idVen)$$

DROP PROCEDURE IF EXISTS `crearVenta`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `crearVenta` (IN `fech` DATETIME, IN `montot` INT, IN `mediop` TEXT)  INSERT INTO ventas(Fecha, MontoTotal, MedioPago) VALUES (fech, montot, mediop)$$

DROP PROCEDURE IF EXISTS `EditarCategoria`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `EditarCategoria` (IN `idCate` INT, IN `Nomb` TEXT)  NO SQL
UPDATE articulos SET Nombre= Nomb
WHERE idCategorias = idCate$$

DROP PROCEDURE IF EXISTS `EliminarArticulo`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `EliminarArticulo` (IN `idArti` INT)  NO SQL
UPDATE articulos SET eliminado = TRUE
WHERE IdArticulos= idArti AND StockActual=0$$

DROP PROCEDURE IF EXISTS `EliminarCategoria`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `EliminarCategoria` (IN `idCateg` INT)  NO SQL
UPDATE categorias SET eliminada= TRUE
WHERE idCategorias = idCateG$$

DROP PROCEDURE IF EXISTS `InsertarCajaFinal`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertarCajaFinal` (IN `final` INT, IN `fech` DATE)  NO SQL
UPDATE caja SET montoFinal= final
where fecha= fech$$

DROP PROCEDURE IF EXISTS `InsertarCajaInicial`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertarCajaInicial` (IN `fecha` DATE, IN `montoCaja` INT)  NO SQL
INSERT INTO caja (Fecha, montoInicial) VALUES(fecha, montoCaja)$$

DROP PROCEDURE IF EXISTS `InsertarMovimiento`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertarMovimiento` (IN `mont` INT, IN `concepto` TEXT, IN `fech` DATETIME)  NO SQL
INSERT INTO movimientos (Monto, Fecha, IdConcepto)VALUES(mont, fech ,concepto)$$

DROP PROCEDURE IF EXISTS `listarArticulos`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `listarArticulos` ()  SELECT * FROM articulos WHERE eliminado= false$$

DROP PROCEDURE IF EXISTS `listarCategorias`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `listarCategorias` ()  select * from categorias WHERE eliminada= false$$

DROP PROCEDURE IF EXISTS `listarDetalleVenta`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `listarDetalleVenta` (IN `ventaa2` INT)  SELECT dv.* , a.Nombre, a.IdArticulos
FROM detalleventa dv
INNER JOIN articulos a ON dv.idArticulo = a.idArticulos
WHERE dv.idVenta= ventaa2$$

DROP PROCEDURE IF EXISTS `ListarMovimientos`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarMovimientos` ()  NO SQL
SELECT m.* , c.Nombre, c.SalidaEntrada
FROM movimientos m INNER JOIN tipoconcepcio c
ON m.IdConcepto = c.IdConcepto$$

DROP PROCEDURE IF EXISTS `listarProductosxCate`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `listarProductosxCate` (IN `cate` INT)  SELECT * FROM articulos where IdCat = cate AND eliminado= false$$

DROP PROCEDURE IF EXISTS `ListarTipoConceptos`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarTipoConceptos` ()  NO SQL
SELECT * FROM tipoconcepcio$$

DROP PROCEDURE IF EXISTS `ListarVentaActual`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarVentaActual` (IN `venti` INT)  NO SQL
SELECT * FROM ventas WHERE idVentas=venti$$

DROP PROCEDURE IF EXISTS `listarVentas`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `listarVentas` ()  BEGIN
SELECT * FROM ventas;
END$$

DROP PROCEDURE IF EXISTS `ListarVentasEfectivoXDia`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarVentasEfectivoXDia` (IN `fech` DATE)  NO SQL
SELECT * FROM ventas WHERE MedioPago= "Efectivo" AND DAY(Fecha)= DAY(fech) AND MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech)$$

DROP PROCEDURE IF EXISTS `ListarVentasMensuales`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarVentasMensuales` (IN `mes` INT, IN `año` INT)  NO SQL
SELECT * FROM ventas WHERE MONTH(Fecha)= mes AND YEAR(Fecha) = año$$

DROP PROCEDURE IF EXISTS `ListarVentasTarjetaXDia`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarVentasTarjetaXDia` (IN `fech` DATE)  NO SQL
SELECT * FROM ventas WHERE MedioPago= "Tarjeta" AND DAY(Fecha)= DAY(fech) AND MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech)$$

DROP PROCEDURE IF EXISTS `ListarVentasxDia`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarVentasxDia` (IN `fech` DATE)  NO SQL
SELECT * FROM ventas WHERE DAY(Fecha)= DAY(fech) AND MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech)$$

DROP PROCEDURE IF EXISTS `nombreProducto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `nombreProducto` (IN `idArti` INT(1))  NO SQL
SELECT nombre FROM articulos WHERE idArticulos = idArti$$

DROP PROCEDURE IF EXISTS `ObtenerCajaExistente`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerCajaExistente` (IN `fech` DATE)  NO SQL
SELECT*  from caja where fecha= fech$$

DROP PROCEDURE IF EXISTS `obtenerCategoria`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerCategoria` (IN `idcategoria` INT)  SELECT Nombre FROM categorias WHERE idCategorias = idcategoria$$

DROP PROCEDURE IF EXISTS `ObtenerDetalleExistente`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerDetalleExistente` (IN `idDetalleV` INT, IN `idventaactual2` INT)  NO SQL
select COUNT(*) from detalleventa WHERE idArticulo = idDetalleV
AND idVenta = idventaactual2$$

DROP PROCEDURE IF EXISTS `obtenerIdVentaActual`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerIdVentaActual` ()  NO SQL
SELECT MAX(idVentas) AS id FROM ventas$$

DROP PROCEDURE IF EXISTS `ObtenerMovimientosEntrada`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerMovimientosEntrada` (IN `fechaa` DATE)  NO SQL
SELECT M.* , c.* 
FROM movimientos M 
INNER JOIN tipoconcepcio C on m.IdConcepto = c.idConcepto
WHERE DAY(Fecha)= DAY(fechaa) 
AND MONTH(Fecha)=MONTH(fechaa) 
AND YEAR(Fecha)=YEAR(fechaa)
AND C.SalidaEntrada= "Ingreso"$$

DROP PROCEDURE IF EXISTS `ObtenerMovimientosSalida`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerMovimientosSalida` (IN `fechaaa` DATE)  NO SQL
SELECT M.* , c.* 
FROM movimientos M 
INNER JOIN tipoconcepcio C on m.IdConcepto = c.idConcepto
WHERE DAY(Fecha)= DAY(fechaaa) 
AND MONTH(Fecha)=MONTH(fechaaa) 
AND YEAR(Fecha)=YEAR(fechaaa)
AND C.SalidaEntrada= "Salida"$$

DROP PROCEDURE IF EXISTS `ObtenerProducto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerProducto` (IN `idArti` INT)  NO SQL
SELECT * FROM articulos WHERE IdArticulos = idArti$$

DROP PROCEDURE IF EXISTS `obtenerStockActual`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerStockActual` (IN `idArtii2` INT)  NO SQL
SELECT StockActual FROM articulos WHERE IdArticulos= idArtii2$$

DROP PROCEDURE IF EXISTS `obtenerSubtotal`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerSubtotal` (IN `idVen` INT)  NO SQL
SELECT SUM(subtotal) AS subt FROM detalleventa WHERE idVenta= idVen$$

DROP PROCEDURE IF EXISTS `ObtenerVentaEfectivoMes`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerVentaEfectivoMes` (IN `fech` DATE)  NO SQL
SELECT SUM(MontoTotal) AS MontoTotal FROM ventas WHERE MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech) AND MedioPago= "Efectivo"$$

DROP PROCEDURE IF EXISTS `ObtenerVentaEfectivoxDia`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerVentaEfectivoxDia` (IN `fech` DATE)  NO SQL
SELECT SUM(MontoTotal) AS MontoTotal FROM ventas WHERE DAY(Fecha)= DAY(fech) AND MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech) AND MedioPago= "Efectivo"$$

DROP PROCEDURE IF EXISTS `ObtenerVentaTarjetaXDia`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerVentaTarjetaXDia` (IN `fech` DATE)  NO SQL
SELECT SUM(MontoTotal) AS MontoTotal FROM ventas WHERE DAY(Fecha)= DAY(fech) AND MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech) AND MedioPago= "Tarjeta"$$

DROP PROCEDURE IF EXISTS `ObtenerVentaTarjetaxMes`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerVentaTarjetaxMes` (IN `fech` DATE)  NO SQL
SELECT SUM(MontoTotal) AS MontoTotal FROM ventas WHERE MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech) AND MedioPago= "Tarjeta"$$

DROP PROCEDURE IF EXISTS `ObtenerVentaTotalXDia`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerVentaTotalXDia` (IN `fech` DATE)  NO SQL
SELECT SUM(MontoTotal) AS Total FROM ventas WHERE DAY(Fecha)= DAY(fech) AND MONTH(Fecha)=MONTH(fech) AND YEAR(Fecha)=YEAR(fech)$$

DROP PROCEDURE IF EXISTS `precioProducto`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `precioProducto` (IN `idArti` INT(2))  BEGIN
SELECT Precio FROM Articulos WHERE idArticulos = idArti;
END$$

DROP PROCEDURE IF EXISTS `RestoStockVenta`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `RestoStockVenta` (IN `stockActu` INT, IN `articId` INT)  NO SQL
UPDATE articulos SET StockActual = stockActu
WHERE IdArticulos = articId$$

DROP PROCEDURE IF EXISTS `ValidarCajaFinal`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ValidarCajaFinal` (IN `fech` DATE)  NO SQL
SELECT* FROM caja WHERE fecha= fech$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos`
--

DROP TABLE IF EXISTS `articulos`;
CREATE TABLE IF NOT EXISTS `articulos` (
  `IdArticulos` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  `IdCat` int(11) NOT NULL,
  `Precio` int(11) NOT NULL,
  `StockActual` int(11) NOT NULL,
  `StockMinimo` int(11) NOT NULL,
  `eliminado` tinyint(4) NOT NULL,
  PRIMARY KEY (`IdArticulos`)
) ENGINE=MyISAM AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `articulos`
--

INSERT INTO `articulos` (`IdArticulos`, `Nombre`, `IdCat`, `Precio`, `StockActual`, `StockMinimo`, `eliminado`) VALUES
(1, 'Remera modal m/l con apliques', 2, 75, 26, 5, 0),
(2, 'Chomba pique blanca', 5, 120, 44, 10, 0),
(3, 'Body m/l azul', 4, 60, 34, 5, 0),
(4, 'enterito c/gorrito', 3, 75, 18, 5, 0),
(5, 'Cochecito automatico', 6, 3500, 15, 1, 0),
(6, 'Chupin de colores', 1, 270, 1, 3, 0),
(7, 'Chupin elastizado con strass', 2, 250, 15, 3, 0),
(8, 'Medias blancas', 5, 35, 15, 2, 0),
(14, 'rmera', 4, 50, 0, 1, 1),
(15, 'rmera', 5, 33, 0, 2, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `caja`
--

DROP TABLE IF EXISTS `caja`;
CREATE TABLE IF NOT EXISTS `caja` (
  `Fecha` date NOT NULL,
  `montoInicial` int(11) DEFAULT NULL,
  `montoFinal` int(11) DEFAULT NULL,
  PRIMARY KEY (`Fecha`),
  UNIQUE KEY `Fecha` (`Fecha`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `caja`
--

INSERT INTO `caja` (`Fecha`, `montoInicial`, `montoFinal`) VALUES
('2016-07-15', 600, NULL),
('2016-08-05', 500, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

DROP TABLE IF EXISTS `categorias`;
CREATE TABLE IF NOT EXISTS `categorias` (
  `idCategorias` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  `eliminada` tinyint(1) NOT NULL,
  PRIMARY KEY (`idCategorias`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`idCategorias`, `Nombre`, `eliminada`) VALUES
(1, 'Nene', 0),
(2, 'Nena', 0),
(3, 'Beba', 0),
(4, 'Bebe', 0),
(5, 'Colegial', 0),
(6, 'Rodados', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleventa`
--

DROP TABLE IF EXISTS `detalleventa`;
CREATE TABLE IF NOT EXISTS `detalleventa` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `idArticulo` int(11) NOT NULL,
  `precio` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `subtotal` int(11) NOT NULL,
  `idVenta` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `ixIdDetalle` (`Id`),
  KEY `FKarticuloDetalles_idx` (`idArticulo`),
  KEY `FKidVentas_idx` (`idVenta`)
) ENGINE=MyISAM AUTO_INCREMENT=16 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `detalleventa`
--

INSERT INTO `detalleventa` (`Id`, `idArticulo`, `precio`, `cantidad`, `subtotal`, `idVenta`) VALUES
(1, 4, 75, 3, 225, 1),
(2, 4, 75, 2, 150, 2),
(3, 4, 75, 3, 225, 3),
(4, 3, 60, 1, 60, 4),
(5, 4, 75, 1, 75, 5),
(6, 7, 250, 1, 250, 6),
(7, 3, 60, 1, 60, 7),
(8, 5, 3500, 1, 3500, 7),
(9, 3, 60, 4, 240, 8),
(10, 2, 120, 2, 240, 8),
(11, 3, 60, 1, 60, 9),
(12, 4, 75, 1, 75, 10),
(13, 4, 75, 2, 150, 11),
(14, 5, 3500, 2, 7000, 12),
(15, 4, 75, 5, 375, 12);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mediosdepago`
--

DROP TABLE IF EXISTS `mediosdepago`;
CREATE TABLE IF NOT EXISTS `mediosdepago` (
  `IdM` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`IdM`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `movimientos`
--

DROP TABLE IF EXISTS `movimientos`;
CREATE TABLE IF NOT EXISTS `movimientos` (
  `IdMovimientos` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `IdConcepto` int(11) NOT NULL,
  `Monto` int(11) NOT NULL,
  PRIMARY KEY (`IdMovimientos`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `movimientos`
--

INSERT INTO `movimientos` (`IdMovimientos`, `Fecha`, `IdConcepto`, `Monto`) VALUES
(1, '2016-06-14 11:28:49', 4, 500),
(2, '2016-06-14 11:30:15', 6, 20),
(3, '2016-07-01 08:18:47', 1, 500),
(4, '2016-07-01 08:54:07', 4, 153),
(5, '2016-07-01 08:54:30', 5, 453),
(6, '2016-07-01 11:13:06', 7, 150),
(8, '2016-07-04 10:49:53', 7, 7375),
(9, '2016-08-05 08:24:24', 1, 500);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedores`
--

DROP TABLE IF EXISTS `proveedores`;
CREATE TABLE IF NOT EXISTS `proveedores` (
  `idProveedores` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `IdArti` int(11) NOT NULL,
  PRIMARY KEY (`idProveedores`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoconcepcio`
--

DROP TABLE IF EXISTS `tipoconcepcio`;
CREATE TABLE IF NOT EXISTS `tipoconcepcio` (
  `idConcepto` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` text NOT NULL,
  `SalidaEntrada` text NOT NULL,
  PRIMARY KEY (`idConcepto`)
) ENGINE=MyISAM AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `tipoconcepcio`
--

INSERT INTO `tipoconcepcio` (`idConcepto`, `Nombre`, `SalidaEntrada`) VALUES
(1, 'Sueldo', 'Salida'),
(2, 'Alquileres', 'Salida'),
(3, 'Luz', 'Salida'),
(4, 'Telefono', 'Salida'),
(5, 'Tarjeta', 'Ingreso'),
(6, 'Insumos', 'Salida'),
(7, 'Venta', 'Ingreso');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE IF NOT EXISTS `usuarios` (
  `idUsuarios` int(11) NOT NULL,
  `NOmbre` varchar(45) NOT NULL,
  PRIMARY KEY (`idUsuarios`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

DROP TABLE IF EXISTS `ventas`;
CREATE TABLE IF NOT EXISTS `ventas` (
  `idVentas` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `MontoTotal` int(11) DEFAULT NULL,
  `MedioPago` text,
  PRIMARY KEY (`idVentas`)
) ENGINE=MyISAM AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `ventas`
--

INSERT INTO `ventas` (`idVentas`, `Fecha`, `MontoTotal`, `MedioPago`) VALUES
(1, '2016-06-14 11:23:13', 225, 'Efectivo'),
(2, '2016-06-23 14:50:41', 150, 'Efectivo'),
(3, '2016-06-23 14:53:35', 225, 'Efectivo'),
(4, '2016-06-23 14:54:22', 60, 'Tarjeta'),
(5, '2016-06-23 15:06:58', 75, 'Efectivo'),
(6, '2016-07-01 08:06:59', 250, 'Efectivo'),
(7, '2016-07-01 08:48:05', 3560, 'Tarjeta'),
(8, '2016-07-01 08:52:32', 480, 'Tarjeta'),
(9, '2016-07-01 10:23:04', 60, 'Efectivo'),
(10, '2016-07-01 11:12:11', 75, 'Efectivo'),
(11, '2016-07-01 11:13:06', 150, 'Efectivo'),
(12, '2016-07-04 10:49:53', 7375, 'Efectivo');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
