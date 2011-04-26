create database Analit;

CREATE TABLE `Analit`.`IVRNContent` (
  `Id` INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  `Content` LONGTEXT NOT NULL,
  `ViewName` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = InnoDB;

CREATE TABLE `Analit`.`MenuField` (
  `Id` INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Link` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = InnoDB;

CREATE TABLE `Analit`.`SubMenuField` (
  `Id` INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  `MenuField` INT(10) UNSIGNED,
  `Name` VARCHAR(45) NOT NULL,
  `Link` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `FK_SubMenuField_1` FOREIGN KEY `FK_SubMenuField_1` (`MenuField`)
    REFERENCES `MenuField` (`Id`)
    ON DELETE SET NULL
    ON UPDATE CASCADE
)
ENGINE = InnoDB;


ALTER TABLE `Analit`.`SubMenuField`
 DROP FOREIGN KEY `FK_SubMenuField_1`;

ALTER TABLE `Analit`.`SubMenuField` ADD CONSTRAINT `FK_SubMenuField_1` FOREIGN KEY `FK_SubMenuField_1` (`MenuField`)
    REFERENCES `MenuField` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

