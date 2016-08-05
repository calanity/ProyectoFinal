-- phpMyAdmin SQL Dump
-- version 3.5.1
-- http://www.phpmyadmin.net
--
-- Servidor: localhost
-- Tiempo de generación: 03-06-2016 a las 02:30:49
-- Versión del servidor: 5.5.24-log
-- Versión de PHP: 5.4.3

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `basedatos`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `ActualizarProductoDV`(IN `canti` INT, IN `subti` INT, IN `idVen` INT, IN `idArtic` INT)
    NO SQL
UPDATE detalleventa SET cantidad = canti , subtotal = subti
WHERE idVenta = idVen AND idArticulo= idArtic$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `actualizarVenta`(IN `tot` INT, IN `idVen` INT)
    NO SQL
UPDATE ventas SET MontoTotal = tot 
WHERE idVentas = idVen$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `crearArticulo`()
BEGIN
INSERT INTO articulos (Nombre, Idcat, Precio, StockActual, StockMinimo)  VALUES ( Nomb, cate, prec, stkActual, stkMinimo);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `crearDetalleVenta`(IN `idArti` INT, IN `prec` INT, IN `canti` INT, IN `subt` INT, IN `idVen` INT)
    NO SQL
INSERT INTO detalleventa (idArticulo , precio, cantidad, subtotal, idVenta) VALUES (idArti, prec, canti, subt, idVen)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `crearVenta`(IN `fech` DATETIME)
INSERT INTO ventas(Fecha) VALUES (fech)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `listarArticulos`()
BEGIN
SELECT * FROM articulos;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `listarCategorias`()
BEGIN
select * from categorias;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `listarDetalleVenta`(IN `ventaa2` INT)
SELECT dv.* , a.Nombre, a.IdArticulos
FROM detalleventa dv
INNER JOIN articulos a ON dv.idArticulo = a.idArticulos
WHERE dv.idVenta= ventaa2$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `listarProductosxCate`(IN `cate` INT)
BEGIN
SELECT * FROM articulos where IdCat = cate; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ListarVentaActual`(IN `venti` INT)
    NO SQL
SELECT * FROM ventas WHERE idVentas=venti$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `listarVentas`()
BEGIN
SELECT * FROM ventas;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `nombreProducto`(IN `idArti` INT(1))
    NO SQL
SELECT nombre FROM articulos WHERE idArticulos = idArti$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerCategoria`()
BEGIN
SELECT Nombre FROM categorias WHERE Id = idcategoria; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtenerDetalleExistente`(IN `idDetalleV` INT, IN `idventaactual2` INT)
    NO SQL
select COUNT(*) from detalleventa WHERE idArticulo = idDetalleV
AND idVenta = idventaactual2$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerIdVA`()
    NO SQL
SELECT Nombre FROM idventaactual$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerIdVentaActual`()
    NO SQL
SELECT MAX(idVentas) AS id FROM ventas$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerStockActual`(IN `idArtii2` INT)
    NO SQL
SELECT StockActual FROM articulos WHERE IdArticulos= idArtii2$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `obtenerSubtotal`(IN `idVen` INT)
    NO SQL
SELECT SUM(subtotal) AS subt FROM detalleventa WHERE idVenta= idVen$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `precioProducto`(IN `idArti` INT(2))
BEGIN
SELECT Precio FROM Articulos WHERE idArticulos = idArti;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `RestoStockVenta`(IN `stockActu` INT, IN `articId` INT)
    NO SQL
UPDATE articulos SET StockActual = stockActu
WHERE IdArticulos = articId$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `articulos`
--

CREATE TABLE IF NOT EXISTS `articulos` (
  `IdArticulos` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  `IdCat` int(11) NOT NULL,
  `Precio` int(11) NOT NULL,
  `StockActual` int(11) NOT NULL,
  `StockMinimo` int(11) NOT NULL,
  PRIMARY KEY (`IdArticulos`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Volcado de datos para la tabla `articulos`
--

INSERT INTO `articulos` (`IdArticulos`, `Nombre`, `IdCat`, `Precio`, `StockActual`, `StockMinimo`) VALUES
(1, 'Remera modal m/l con apliques', 2, 75, 34, 5),
(2, 'Chomba pique blanca', 5, 120, 50, 10),
(3, 'Body m/l azul', 4, 60, 35, 5),
(4, 'enterito c/gorrito', 3, 75, 25, 5),
(5, 'Cochecito automatico', 6, 3500, 3, 1),
(6, 'Chupin de colores', 1, 270, 15, 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE IF NOT EXISTS `categorias` (
  `idCategorias` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idCategorias`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`idCategorias`, `Nombre`) VALUES
(1, 'Nene'),
(2, 'Nena'),
(3, 'Beba'),
(4, 'Bebe'),
(5, 'Colegial'),
(6, 'Rodados');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detalleventa`
--

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
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `idventaactual`
--

CREATE TABLE IF NOT EXISTS `idventaactual` (
  `Nombre` int(11) NOT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `mediosdepago`
--

CREATE TABLE IF NOT EXISTS `mediosdepago` (
  `IdM` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`IdM`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedores`
--

CREATE TABLE IF NOT EXISTS `proveedores` (
  `idProveedores` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `IdArti` int(11) NOT NULL,
  PRIMARY KEY (`idProveedores`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE IF NOT EXISTS `usuarios` (
  `idUsuarios` int(11) NOT NULL,
  `NOmbre` varchar(45) NOT NULL,
  PRIMARY KEY (`idUsuarios`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventas`
--

CREATE TABLE IF NOT EXISTS `ventas` (
  `idVentas` int(11) NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `MontoTotal` int(11) DEFAULT NULL,
  `MedioPago` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`idVentas`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
