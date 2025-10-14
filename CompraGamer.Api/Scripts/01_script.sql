-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 14-10-2025 a las 00:11:15
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `gestionmicros`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `chico`
--

CREATE TABLE `chico` (
  `dni` char(8) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `apellido` varchar(50) DEFAULT NULL,
  `micro_patente` char(7) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `chofer`
--

CREATE TABLE `chofer` (
  `dni` char(8) NOT NULL,
  `nombre` varchar(50) DEFAULT NULL,
  `apellido` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `microescolar`
--

CREATE TABLE `microescolar` (
  `patente` char(7) NOT NULL,
  `chofer_dni` char(8) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `chico`
--
ALTER TABLE `chico`
  ADD PRIMARY KEY (`dni`),
  ADD KEY `micro_patente` (`micro_patente`);

--
-- Indices de la tabla `chofer`
--
ALTER TABLE `chofer`
  ADD PRIMARY KEY (`dni`);

--
-- Indices de la tabla `microescolar`
--
ALTER TABLE `microescolar`
  ADD PRIMARY KEY (`patente`),
  ADD UNIQUE KEY `chofer_dni` (`chofer_dni`);

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `chico`
--
ALTER TABLE `chico`
  ADD CONSTRAINT `chico_ibfk_1` FOREIGN KEY (`micro_patente`) REFERENCES `microescolar` (`patente`);

--
-- Filtros para la tabla `microescolar`
--
ALTER TABLE `microescolar`
  ADD CONSTRAINT `microescolar_ibfk_1` FOREIGN KEY (`chofer_dni`) REFERENCES `chofer` (`dni`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
