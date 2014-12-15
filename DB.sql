-- phpMyAdmin SQL Dump
-- version 4.3.2
-- httpwww.phpmyadmin.net
--
-- Client   127.0.0.13306
-- Généré le   Lun 15 Décembre 2014 à 2101
-- Version du serveur   5.6.22
-- Version de PHP   5.5.9-1ubuntu4.5

--
-- Base de données   `centresportif420`
--

-- --------------------------------------------------------

--
-- Structure de la table `abonnement`
--

CREATE TABLE `abonnement` (
  `idabonnement` int(11) NOT NULL,
  `idpersonne` int(11) NOT NULL,
  `idgroupe` int(11) NOT NULL,
  `dateinscription` date NOT NULL,
  `datefin` date NOT NULL,
  `prix` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `activite`
--

CREATE TABLE `activite` (
  `idactivite` int(11) NOT NULL,
  `nom` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `duree` int(11) NOT NULL DEFAULT '1',
  `description` text COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `addresse`
--

CREATE TABLE `addresse` (
  `idaddresse` int(11) NOT NULL,
  `numero` int(11) NOT NULL,
  `rue` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `codepostal` varchar(6) COLLATE utf8_unicode_ci NOT NULL,
  `ville` varchar(25) COLLATE utf8_unicode_ci NOT NULL,
  `pays` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `idpersonne` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `enseigne`
--

CREATE TABLE `enseigne` (
  `idenseigne` int(11) NOT NULL,
  `idpersonne` int(11) NOT NULL,
  `idgroupe` int(11) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `groupe`
--

CREATE TABLE `groupe` (
  `idgroupe` int(11) NOT NULL,
  `idactivite` int(11) NOT NULL,
  `numerogroupe` varchar(11) COLLATE utf8_unicode_ci NOT NULL,
  `prix` decimal(6,2) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `message`
--

CREATE TABLE `message` (
  `idmessage` int(11) NOT NULL,
  `idpersonne` int(11) NOT NULL,
  `contenu` varchar(255) NOT NULL,
  `datecreation` datetime NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `paiement`
--

CREATE TABLE `paiement` (
  `idpaiement` int(11) NOT NULL,
  `idpersonne` int(11) NOT NULL,
  `date` date NOT NULL,
  `montant` decimal(10,0) NOT NULL,
  `mode` varchar(10) NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `personne`
--

CREATE TABLE `personne` (
  `idpersonne` int(11) NOT NULL,
  `prenom` varchar(25) COLLATE utf8_unicode_ci NOT NULL,
  `nom` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `sexe` char(1) COLLATE utf8_unicode_ci NOT NULL,
  `datenaissance` date NOT NULL,
  `email` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `motdepasse` varchar(16) COLLATE utf8_unicode_ci NOT NULL,
  `codebarre` varchar(10) COLLATE utf8_unicode_ci NOT NULL,
  `role` varchar(10) COLLATE utf8_unicode_ci NOT NULL DEFAULT 'membre'
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `presence`
--

CREATE TABLE `presence` (
  `idpresence` int(11) NOT NULL DEFAULT '0',
  `idpersonne` int(11) NOT NULL,
  `idseance` int(11) NOT NULL,
  `present` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- Structure de la table `seance`
--

CREATE TABLE `seance` (
  `idseance` int(11) NOT NULL,
  `idgroupe` int(11) NOT NULL,
  `datedebut` datetime NOT NULL,
  `datefin` datetime NOT NULL
) ENGINE=InnoDB AUTO_INCREMENT=87 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Index pour les tables exportées
--

--
-- Index pour la table `abonnement`
--
ALTER TABLE `abonnement`
  ADD PRIMARY KEY (`idabonnement`), ADD KEY `idpersonne` (`idpersonne`), ADD KEY `fk_abonnement_idgroupe` (`idgroupe`);

--
-- Index pour la table `activite`
--
ALTER TABLE `activite`
  ADD PRIMARY KEY (`idactivite`);

--
-- Index pour la table `addresse`
--
ALTER TABLE `addresse`
  ADD PRIMARY KEY (`idaddresse`), ADD KEY `fk_addresse_idpersonne` (`idpersonne`);

--
-- Index pour la table `enseigne`
--
ALTER TABLE `enseigne`
  ADD PRIMARY KEY (`idenseigne`), ADD KEY `fk_idgroupe` (`idgroupe`), ADD KEY `fk_enseigne_idpersonne` (`idpersonne`);

--
-- Index pour la table `groupe`
--
ALTER TABLE `groupe`
  ADD PRIMARY KEY (`idgroupe`), ADD KEY `idactivite` (`idactivite`);

--
-- Index pour la table `message`
--
ALTER TABLE `message`
  ADD PRIMARY KEY (`idmessage`), ADD KEY `fk_message_idpersonne` (`idpersonne`);

--
-- Index pour la table `paiement`
--
ALTER TABLE `paiement`
  ADD PRIMARY KEY (`idpaiement`), ADD KEY `fk_paiement_idpersonne` (`idpersonne`);

--
-- Index pour la table `personne`
--
ALTER TABLE `personne`
  ADD PRIMARY KEY (`idpersonne`);

--
-- Index pour la table `presence`
--
ALTER TABLE `presence`
  ADD PRIMARY KEY (`idpresence`), ADD KEY `idseance` (`idseance`), ADD KEY `fk_presence_idpersonne` (`idpersonne`);

--
-- Index pour la table `seance`
--
ALTER TABLE `seance`
  ADD PRIMARY KEY (`idseance`), ADD KEY `fk_idgroupeSeance` (`idgroupe`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `abonnement`
--
ALTER TABLE `abonnement`
  MODIFY `idabonnement` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT pour la table `activite`
--
ALTER TABLE `activite`
  MODIFY `idactivite` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT pour la table `addresse`
--
ALTER TABLE `addresse`
  MODIFY `idaddresse` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT pour la table `enseigne`
--
ALTER TABLE `enseigne`
  MODIFY `idenseigne` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT pour la table `groupe`
--
ALTER TABLE `groupe`
  MODIFY `idgroupe` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT pour la table `message`
--
ALTER TABLE `message`
  MODIFY `idmessage` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT pour la table `paiement`
--
ALTER TABLE `paiement`
  MODIFY `idpaiement` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT pour la table `personne`
--
ALTER TABLE `personne`
  MODIFY `idpersonne` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- AUTO_INCREMENT pour la table `seance`
--
ALTER TABLE `seance`
  MODIFY `idseance` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=22;
--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `abonnement`
--
ALTER TABLE `abonnement`
ADD CONSTRAINT `fk_abonnement_idgroupe` FOREIGN KEY (`idgroupe`) REFERENCES `groupe` (`idgroupe`) ON DELETE CASCADE ON UPDATE CASCADE,
ADD CONSTRAINT `fk_abonnement_idpersonne` FOREIGN KEY (`idpersonne`) REFERENCES `personne` (`idpersonne`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `addresse`
--
ALTER TABLE `addresse`
ADD CONSTRAINT `fk_addresse_idpersonne` FOREIGN KEY (`idpersonne`) REFERENCES `personne` (`idpersonne`) ON DELETE CASCADE;

--
-- Contraintes pour la table `enseigne`
--
ALTER TABLE `enseigne`
ADD CONSTRAINT `fk_enseigne_idpersonne` FOREIGN KEY (`idpersonne`) REFERENCES `personne` (`idpersonne`),
ADD CONSTRAINT `fk_idgroupe` FOREIGN KEY (`idgroupe`) REFERENCES `groupe` (`idgroupe`) ON DELETE CASCADE;

--
-- Contraintes pour la table `groupe`
--
ALTER TABLE `groupe`
ADD CONSTRAINT `groupe_ibfk_1` FOREIGN KEY (`idactivite`) REFERENCES `activite` (`idactivite`);

--
-- Contraintes pour la table `message`
--
ALTER TABLE `message`
ADD CONSTRAINT `fk_message_idpersonne` FOREIGN KEY (`idpersonne`) REFERENCES `personne` (`idpersonne`) ON DELETE CASCADE;

--
-- Contraintes pour la table `paiement`
--
ALTER TABLE `paiement`
ADD CONSTRAINT `fk_paiement_idpersonne` FOREIGN KEY (`idpersonne`) REFERENCES `personne` (`idpersonne`) ON DELETE CASCADE;

--
-- Contraintes pour la table `presence`
--
ALTER TABLE `presence`
ADD CONSTRAINT `fk_presence_idpersonne` FOREIGN KEY (`idpersonne`) REFERENCES `personne` (`idpersonne`) ON DELETE CASCADE,
ADD CONSTRAINT `presence_ibfk_1` FOREIGN KEY (`idpersonne`) REFERENCES `personne` (`idpersonne`),
ADD CONSTRAINT `presence_ibfk_2` FOREIGN KEY (`idseance`) REFERENCES `seance` (`idseance`);

--
-- Contraintes pour la table `seance`
--
ALTER TABLE `seance`
ADD CONSTRAINT `fk_idgroupeSeance` FOREIGN KEY (`idgroupe`) REFERENCES `groupe` (`idgroupe`) ON DELETE CASCADE;
