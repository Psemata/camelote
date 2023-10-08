-- phpMyAdmin SQL Dump
-- version 4.6.6deb5ubuntu0.5
-- https://www.phpmyadmin.net/
--
-- Client :  localhost:3306
-- Généré le :  Sam 22 Janvier 2022 à 12:57
-- Version du serveur :  5.7.36-0ubuntu0.18.04.1
-- Version de PHP :  7.2.24-0ubuntu0.18.04.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `camelote`
--

-- --------------------------------------------------------

--
-- Structure de la table `castle`
--

CREATE TABLE `castle` (
  `id` int(11) NOT NULL,
  `level` int(11) NOT NULL,
  `latitude` double NOT NULL,
  `longitude` double NOT NULL,
  `start_time` int(11) NOT NULL,
  `end_time` int(11) NOT NULL,
  `owner` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Table comprenant le château de l''utilisateur';

--
-- Contenu de la table `castle`
--

INSERT INTO `castle` (`id`, `level`, `latitude`, `longitude`, `start_time`, `end_time`, `owner`) VALUES
(1, 2, 0, 0, 0, 0, 3),
(2, 1, 0, 0, 0, 0, 5),
(3, 1, 0, 0, 0, 0, 15);

-- --------------------------------------------------------

--
-- Structure de la table `interest_point`
--

CREATE TABLE `interest_point` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `latitude` float NOT NULL,
  `longitude` float NOT NULL,
  `start_time` int(11) NOT NULL,
  `end_time` int(11) NOT NULL,
  `is_arena` tinyint(4) NOT NULL COMMENT '0 -> n''est pas une arène\r\n1 -> est une arène',
  `image` varchar(255) NOT NULL COMMENT 'Chemin de l''image du point d''intérêt'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `interest_point`
--

INSERT INTO `interest_point` (`id`, `name`, `latitude`, `longitude`, `start_time`, `end_time`, `is_arena`, `image`) VALUES
(1, 'Rue de la Fourchaux 17', 47.1493, 6.98785, 0, 0, 0, '-'),
(2, 'Place du marché', 47.1521, 6.99595, 0, 0, 0, '-');

-- --------------------------------------------------------

--
-- Structure de la table `resources`
--

CREATE TABLE `resources` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `resources`
--

INSERT INTO `resources` (`id`, `name`) VALUES
(1, 'Blé'),
(2, 'Fer'),
(3, 'Pierre'),
(4, 'Nourriture'),
(5, 'Bois'),
(6, 'Or');

-- --------------------------------------------------------

--
-- Structure de la table `resources_to_user`
--

CREATE TABLE `resources_to_user` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `resources` int(11) NOT NULL,
  `quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `resources_to_user`
--

INSERT INTO `resources_to_user` (`id`, `user_id`, `resources`, `quantity`) VALUES
(1, 1, 1, 10),
(2, 1, 6, 150),
(14, 15, 1, 12),
(15, 15, 2, 2),
(16, 15, 3, 8),
(17, 15, 4, 15),
(18, 15, 5, 0),
(19, 15, 6, 20),
(20, 3, 1, 196),
(24, 3, 4, 257),
(26, 3, 3, 17),
(27, 3, 6, 5),
(28, 3, 2, 29),
(29, 3, 5, 13);

-- --------------------------------------------------------

--
-- Structure de la table `unit`
--

CREATE TABLE `unit` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `type` int(11) NOT NULL COMMENT 'Type : Archer (0), Épéiste (1), Lancier (2)',
  `strength` int(11) NOT NULL COMMENT 'Value : 1-40',
  `endurance` int(11) NOT NULL COMMENT 'Value : 1-40',
  `potential` int(11) NOT NULL COMMENT 'Value : 1-40',
  `level` int(11) NOT NULL COMMENT 'Value : 1-20',
  `owner` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Table comprenant les unités du jeu';

--
-- Contenu de la table `unit`
--

INSERT INTO `unit` (`id`, `name`, `type`, `strength`, `endurance`, `potential`, `level`, `owner`) VALUES
(1, 'Cécil', 1, 15, 15, 20, 20, 1);

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `pseudo` varchar(255) NOT NULL,
  `surname` varchar(255) NOT NULL COMMENT 'Nom de famille',
  `name` varchar(255) NOT NULL COMMENT 'Prénom',
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL COMMENT 'Mot de passe crypté'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Contenu de la table `user`
--

INSERT INTO `user` (`id`, `pseudo`, `surname`, `name`, `email`, `password`) VALUES
(1, 'Psemata', 'Bruno', 'Costa', 'bruno.costa@he-arc.ch', '1234'),
(2, 'Ultrasic', 'Diogo', 'Lopes Da Silva', 'diogo.lopesdas@he-arc.ch', '$2y$10$gNNwWHO.3Z5UDlZYUbM.ZOsganPNqNdEAQDtrMhLu86NVPFN3MqUK'),
(3, 'Tino', 'Izzo', 'Valentino', 'valentino.izzo@he-arc.ch', '$2y$10$AB/70mXfZvbzQa5KEPhIpe3LMIJvyVl04QQL11K7yc6kfeHIIzPcu'),
(5, 'test', 'test', 'test', 'test.test@test,ch', '$2y$10$mhdJu.tMUk4cZzzVU800zepWLUCpw7FyEa6DFSESxhhGkMiZlcaNq'),
(15, 'testUser', 'testUser', 'testUser', 'testUser.izzo@he-arc.ch', '$2y$10$UseHhsJr12wHZLVYx83VgexHcn62CVpxp6dKZDetNQYd5nYbdGWxG');

--
-- Index pour les tables exportées
--

--
-- Index pour la table `castle`
--
ALTER TABLE `castle`
  ADD PRIMARY KEY (`id`),
  ADD KEY `owner_castle_fk` (`owner`);

--
-- Index pour la table `interest_point`
--
ALTER TABLE `interest_point`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `resources`
--
ALTER TABLE `resources`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `resources_to_user`
--
ALTER TABLE `resources_to_user`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id_owner_fk` (`user_id`),
  ADD KEY `resources_fk` (`resources`);

--
-- Index pour la table `unit`
--
ALTER TABLE `unit`
  ADD PRIMARY KEY (`id`),
  ADD KEY `owner_unit_fk` (`owner`);

--
-- Index pour la table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `castle`
--
ALTER TABLE `castle`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT pour la table `interest_point`
--
ALTER TABLE `interest_point`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT pour la table `resources`
--
ALTER TABLE `resources`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT pour la table `resources_to_user`
--
ALTER TABLE `resources_to_user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;
--
-- AUTO_INCREMENT pour la table `unit`
--
ALTER TABLE `unit`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT pour la table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `castle`
--
ALTER TABLE `castle`
  ADD CONSTRAINT `owner_castle_fk` FOREIGN KEY (`owner`) REFERENCES `user` (`id`);

--
-- Contraintes pour la table `resources_to_user`
--
ALTER TABLE `resources_to_user`
  ADD CONSTRAINT `resources_fk` FOREIGN KEY (`resources`) REFERENCES `resources` (`id`),
  ADD CONSTRAINT `user_id_owner_fk` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`);

--
-- Contraintes pour la table `unit`
--
ALTER TABLE `unit`
  ADD CONSTRAINT `owner_unit_fk` FOREIGN KEY (`owner`) REFERENCES `user` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
