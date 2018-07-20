-- CREATE TABLE users (
--     id VARCHAR(255) NOT NULL,
--     username VARCHAR(20) NOT NULL,
--     email VARCHAR(255) NOT NULL,
--     password VARCHAR(255) NOT NULL,
--     PRIMARY KEY (id),
--     UNIQUE KEY email (email)
-- );

-- CREATE TABLE vaults (
--   id int NOT NULL AUTO_INCREMENT,
--   title VARCHAR(20) NOT NULL,
--   authorId VARCHAR(255) NOT NULL,
--   PRIMARY KEY (id),
--   INDEX (authorId),

--   FOREIGN KEY (authorId)
--     REFERENCES users(id)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE keeps (
--   id int NOT NULL AUTO_INCREMENT,
--   description VARCHAR(255),
--   img VARCHAR(255) NOT NULL,
--   author VARCHAR(20) NOT NULL,
--   authorId VARCHAR(255) NOT NULL,
--   vaultId int NOT NULL,
--   views int NOT NULL,
--   keeps int NOT NULL,
--   public BOOLEAN NOT NULL,
--   PRIMARY KEY (id),

--   FOREIGN KEY (authorId)
--     REFERENCES users(id)
--     ON DELETE CASCADE,
  
--   FOREIGN KEY (vaultId)
--     REFERENCES vaults(id)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE tags (
--   id int NOT NULL AUTO_INCREMENT,
--   tagName VARCHAR(20) NOT NULL,
--   authorId VARCHAR(255) NOT NULL,
--   keepId int NOT NULL,
--   PRIMARY KEY (id),

--   FOREIGN KEY (authorId)
--     REFERENCES users(id)
--     ON DELETE CASCADE,

--   FOREIGN KEY (keepId)
--     REFERENCES keeps(id)
--     ON DELETE CASCADE
-- );

-- CREATE TABLE shares (
--     id int NOT NULL AUTO_INCREMENT,
--     vaultId int NOT NULL,
--     keepId int NOT NULL,
--     authorId VARCHAR(255) NOT NULL,

--     PRIMARY KEY (id),
--     INDEX (vaultId, keepId),
--     INDEX (authorId),

--     FOREIGN KEY (authorId)
--         REFERENCES users(id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (vaultId)
--         REFERENCES vaults(id)
--         ON DELETE CASCADE,

--     FOREIGN KEY (keepId)
--         REFERENCES keeps(id)
--         ON DELETE CASCADE
-- );
