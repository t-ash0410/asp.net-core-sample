DROP DATABASE IF EXISTS asp_net_sample;
CREATE DATABASE asp_net_sample;
USE asp_net_sample;

DROP TABLE IF EXISTS books;
CREATE TABLE books (
    `id` MEDIUMINT NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(255) NOT NULL,
    `description` TEXT NOT NULL,
    `category` TEXT NOT NULL,
    PRIMARY KEY (id)
)