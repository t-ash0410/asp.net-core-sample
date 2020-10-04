DROP DATABASE IF EXISTS asp_net_sample;
CREATE DATABASE IF NOT EXISTS asp_net_sample DEFAULT CHARACTER SET utf8;

DROP TABLE IF EXISTS books;
CREATE IF NOT EXISTS TABLE books (
     `id` MEDIUMINT NOT NULL AUTO_INCREMENT,
     `name` VARCHAR(255) NOT NULL,
     `description` TEXT NOT NULL,
     `category` TEXT NOT NULL,
     PRIMARY KEY (id)
);

INSERT INTO books
(
	`name`,
  `description`,
  `category`
)
VALUES
(
  'book1',
  'test book',
  'category1'
),
(
  'book2',
  'test book2',
  '分類2'
),
(
  '本３',
  'テスト用の本',
  'category1'
)
;
