CREATE DATABASE IF NOT EXISTS GameStoreMVC
    CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE GameStoreMVC;

CREATE TABLE IF NOT EXISTS Usuarios (
    Id        INT          NOT NULL AUTO_INCREMENT,
    Nome      VARCHAR(150) NOT NULL,
    Email     VARCHAR(200) NOT NULL UNIQUE,
    SenhaHash VARCHAR(255) NOT NULL,
    IsAdmin   TINYINT(1)   NOT NULL DEFAULT 0,
    PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS Games (
    Id         INT            NOT NULL AUTO_INCREMENT,
    Nome       VARCHAR(150)   NOT NULL,
    Descricao  TEXT           NOT NULL,
    Preco      DECIMAL(10,2)  NOT NULL,
    ImagemUrl  VARCHAR(500)   NULL,
    Categoria  VARCHAR(50)    NOT NULL DEFAULT 'Ação',
    EmDestaque TINYINT(1)     NOT NULL DEFAULT 0,
    CriadoEm  DATETIME       NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Admin padrão — senha: Admin@123
INSERT INTO Usuarios (Nome, Email, SenhaHash, IsAdmin) VALUES
('Administrador', 'admin@gamestore.com',
 '$2a$11$ow5QsWtLqDlVNVCd6Y7gK.8Z5r3pL1ULJPqijVFcSmA8Fa3.gLfGS', 1)
ON DUPLICATE KEY UPDATE Id = Id;

-- Jogos de exemplo
INSERT INTO Games (Nome, Descricao, Preco, ImagemUrl, Categoria, EmDestaque) VALUES
('Mario Bros',     'O clássico plataformer da Nintendo.',         2500.00, NULL, 'Aventura', 1),
('The Last of Us', 'Ação e sobrevivência pós-apocalíptica.',       299.90, NULL, 'Ação',    0),
('Gran Turismo 7', 'Simulador de corrida definitivo.',             199.90, NULL, 'Corrida', 0),
('Elden Ring',     'RPG de mundo aberto desafiador.',              249.90, NULL, 'RPG',     0)
ON DUPLICATE KEY UPDATE Id = Id;