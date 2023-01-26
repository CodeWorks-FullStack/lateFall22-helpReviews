-- Active: 1674751345100@@SG-marred-dish-4573-7100-mysql-master.servers.mongodirector.com@3306@freebie
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

ALTER TABLE accounts
  ADD COLUMN brandImg VARCHAR(255) NOT NULL DEFAULT "https://images.unsplash.com/photo-1619454016518-697bc231e7cb?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80";


-- SECTION Restaurants

CREATE TABLE restaurants(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL DEFAULT "Big B. Cheese",
  description VARCHAR(255) NOT NULL DEFAULT "Bring the whole family, witness the rat!",
  ownerId VARCHAR(255) NOT NULL,
  coverImg VARCHAR(255) NOT NULL DEFAULT "https://images.unsplash.com/photo-1552566626-52f8b828add9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
  exposure INT NOT NULL DEFAULT 0,
  shutdown bool NOT NULL DEFAULT false,

  Foreign Key (ownerId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

DROP TABLE restaurants;

INSERT INTO restaurants
(name, `ownerId`, `coverImg`)
VALUES
('Big B. Cheese, Boise', '6216b36ebc31a249987812b1', 'https://images.unsplash.com/photo-1560317255-0579e547559d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80');
INSERT INTO restaurants
(name, `ownerId`, `coverImg`, description)
VALUES
('Sizzle Fizzle', '634844a08c9d1ba02348913d', 'https://images.unsplash.com/photo-1512152272829-e3139592d56f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80' ,"Lorizzle you son of a bizzle dolizzle check out this amizzle, boofron adipiscing its fo rizzle. Nullam things velizzle, aliquet volutpizzle, dang mofo.");


INSERT INTO restaurants
(name, `ownerId`, `coverImg`, description)
VALUES
('PB and Jakes', '634844a08c9d1ba02348913d', 'https://images.unsplash.com/photo-1603701972178-96760b471be9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=764&q=80' ,"We gonna keep this a bit more pg");

-- SECTION Reports

CREATE TABLE reports(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(50) NOT NULL DEFAULT "Report",
  body TEXT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  restaurantId INT NOT NULL,

  Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  Foreign Key (restaurantId) REFERENCES restaurants(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';


-- REVIEW --  HEY YOU ARE GOING TO NEED THIS FOR THE FINAL 😜
-- SELECT 
--   r.*,
--   COUNT(rep.id) AS reportCount
-- FROM restaurants r 
-- JOIN reports rep ON rep.restaurantId = r.id
-- WHERE r.id = 1;

-- SELECT * FROM reports WHERE `restaurantId` = 1;







INSERT INTO reports
(title, `creatorId`, `restaurantId`, body)
VALUES
('This food sucks', '631b5b5fa7f0b66bb817725a', 1, 'Ordered a Cheese pizza, got a dead rat in a cheese blanket on a square piece of cardboard. Im not exagerrating, it was literally a dead rat on a piece of cardboard, and was tucked into a mozzarella quilted blanket. Kinda cool i guess but I have had better.');