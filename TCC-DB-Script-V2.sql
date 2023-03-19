CREATE TABLE TiposContato (
idTipoContato INT NOT NULL,
dsTipoContato VARCHAR(25) NOT NULL,

CONSTRAINT TiposContato_PK PRIMARY KEY (idTipoContato)
)

INSERT INTO TiposContato VALUES (1,'Telefone');
INSERT INTO TiposContato VALUES (2,'eMail'); 
INSERT INTO TiposContato VALUES (3,'WhatsApp'); 

GO

CREATE TABLE Logradouros (
idLogradouro INT NOT NULL,
dsLogradouro VARCHAR(15) NOT NULL,

CONSTRAINT Logradouros_PK PRIMARY KEY (idLogradouro),
)

INSERT INTO Logradouros VALUES (1,'Aeroporto');
INSERT INTO Logradouros VALUES (2,'Alameda');
INSERT INTO Logradouros VALUES (3,'Area');
INSERT INTO Logradouros VALUES (4,'Avenida');
INSERT INTO Logradouros VALUES (5,'Campo');
INSERT INTO Logradouros VALUES (6,'Ch�cara');
INSERT INTO Logradouros VALUES (7,'Col�nia');
INSERT INTO Logradouros VALUES (8,'Condom�nio');
INSERT INTO Logradouros VALUES (9,'Conjunto');
INSERT INTO Logradouros VALUES (10,'Distrito');
INSERT INTO Logradouros VALUES (11,'Esplanada');
INSERT INTO Logradouros VALUES (12,'Esta��o');
INSERT INTO Logradouros VALUES (13,'Estrada');
INSERT INTO Logradouros VALUES (14,'Favela');
INSERT INTO Logradouros VALUES (15,'Fazenda');
INSERT INTO Logradouros VALUES (16,'Feira');
INSERT INTO Logradouros VALUES (17,'Jardim');
INSERT INTO Logradouros VALUES (18,'Ladeira');
INSERT INTO Logradouros VALUES (19,'Lago');
INSERT INTO Logradouros VALUES (20,'Lagoa');
INSERT INTO Logradouros VALUES (21,'Largo');
INSERT INTO Logradouros VALUES (22,'Loteamento');
INSERT INTO Logradouros VALUES (23,'Morro');
INSERT INTO Logradouros VALUES (24,'N�cleo');
INSERT INTO Logradouros VALUES (25,'Parque');
INSERT INTO Logradouros VALUES (26,'Passarela');
INSERT INTO Logradouros VALUES (27,'P�tio');
INSERT INTO Logradouros VALUES (28,'Pra�a');
INSERT INTO Logradouros VALUES (29,'Quadra');
INSERT INTO Logradouros VALUES (30,'Recanto');
INSERT INTO Logradouros VALUES (31,'Residencial');
INSERT INTO Logradouros VALUES (32,'Rodovia');
INSERT INTO Logradouros VALUES (33,'Rua');
INSERT INTO Logradouros VALUES (34,'Setor');
INSERT INTO Logradouros VALUES (35,'S�tio');
INSERT INTO Logradouros VALUES (36,'Travessa');
INSERT INTO Logradouros VALUES (37,'Trecho');
INSERT INTO Logradouros VALUES (38,'Trevo');
INSERT INTO Logradouros VALUES (39,'Vale');
INSERT INTO Logradouros VALUES (40,'Vereda');
INSERT INTO Logradouros VALUES (41,'Via');
INSERT INTO Logradouros VALUES (42,'Viaduto');
INSERT INTO Logradouros VALUES (43,'Viela');
INSERT INTO Logradouros VALUES (44,'Vila');

GO

CREATE TABLE Hospital (
idHospital INT NOT NULL,
CNPJ CHAR(18) NOT NULL,
RazaoSocial VARCHAR(60) NOT NULL,
NomeFantasia VARCHAR(30) NOT NULL,
idLogradouro INT NOT NULL,
Endereco VARCHAR(50) NOT NULL,
Numero VARCHAR(10) NOT NULL,
Complemento VARCHAR(15) NOT NULL,
Bairro VARCHAR(25) NOT NULL,
Cidade VARCHAR(25) NOT NULL,
UF CHAR(2) NOT NULL,
CEP CHAr(9) NOT NULL,
Latitude FLOAT NULL,
Longitude FLOAT NULL,

CONSTRAINT Hospital_PK PRIMARY KEY (idHospital),
CONSTRAINT Hospital_FK1 FOREIGN KEY (idLogradouro) REFERENCES Logradouros (idLogradouro)
)

-- CRIAR INSERTS DE HOSPITAIS PARA SIMULAR

GO

CREATE TABLE Contatos (
idHospital INT NOT NULL,
idContato INT NOT NULL,
idTipoContato INT NOT NULL,
dsContato VARCHAR(50) NOT NULL,
inContato VARCHAR(100) NOT NULL,
  
CONSTRAINT Contatos_PK PRIMARY KEY (idHospital, idContato, idTipoContato),
CONSTRAINT Contatos_FK1 FOREIGN KEY (idHospital) REFERENCES Hospital (idHospital),
CONSTRAINT Contatos_FK2 FOREIGN KEY (idTipoContato) REFERENCES TiposContato (idTipoContato),
)

-- CRIAR INSERTS DE CONTATOS PARA SIMULAR

GO

CREATE TABLE Especialidade (
idEspecialidade INT NOT NULL,
dsEspecialidade VARCHAR(40) NOT NULL,

CONSTRAINT Especialidade_PK PRIMARY KEY (idEspecialidade)
)

INSERT INTO Especialidade VALUES (1,'Acupuntura');
INSERT INTO Especialidade VALUES (2,'Alergia e Imunologia');
INSERT INTO Especialidade VALUES (3,'Anestesiologista');
INSERT INTO Especialidade VALUES (4,'Angiologia');
INSERT INTO Especialidade VALUES (5,'Cardiologia');
INSERT INTO Especialidade VALUES (6,'Cirurgia Cardiovascular');
INSERT INTO Especialidade VALUES (7,'Cirurgia de M�o');
INSERT INTO Especialidade VALUES (8,'Cirurgia de Cabe�a e Pesco�o');
INSERT INTO Especialidade VALUES (9,'Cirurgia do Aparelho Digestivo');
INSERT INTO Especialidade VALUES (10,'Cirurgia Geral');
INSERT INTO Especialidade VALUES (11,'Cirurgia Oncol�gica');
INSERT INTO Especialidade VALUES (12,'Cirurgia Pedi�trica');
INSERT INTO Especialidade VALUES (13,'Cirurgia Pl�stica');
INSERT INTO Especialidade VALUES (14,'Cirurgia Tor�cica');
INSERT INTO Especialidade VALUES (15,'Cirurgia Vascular');
INSERT INTO Especialidade VALUES (16,'Cl�nica M�dica');
INSERT INTO Especialidade VALUES (17,'Coloproctologia');
INSERT INTO Especialidade VALUES (18,'Dermatologia');
INSERT INTO Especialidade VALUES (19,'Endocrinologia e Metabologia');
INSERT INTO Especialidade VALUES (20,'Endoscopia');
INSERT INTO Especialidade VALUES (21,'Gastroenterologia');
INSERT INTO Especialidade VALUES (22,'Gen�tica M�dica');
INSERT INTO Especialidade VALUES (23,'Geriatria');
INSERT INTO Especialidade VALUES (24,'Ginecologia e Obstetr�cia');
INSERT INTO Especialidade VALUES (25,'Hematologia e Hemoterapia');
INSERT INTO Especialidade VALUES (26,'Homeopatia');
INSERT INTO Especialidade VALUES (27,'Infectologia');
INSERT INTO Especialidade VALUES (28,'Mastologia');
INSERT INTO Especialidade VALUES (29,'Medicina de Emerg�ncia');
INSERT INTO Especialidade VALUES (30,'Medicina de Fam�lia e Comunidade');
INSERT INTO Especialidade VALUES (31,'Medicina do Trabalho');
INSERT INTO Especialidade VALUES (32,'Medicina de Tr�fego');
INSERT INTO Especialidade VALUES (33,'Medicina Esportiva');
INSERT INTO Especialidade VALUES (34,'Medicina F�sica e Reabilita��o');
INSERT INTO Especialidade VALUES (35,'Medicina Intensiva');
INSERT INTO Especialidade VALUES (36,'Medicina Legal e Per�cia M�dica');
INSERT INTO Especialidade VALUES (37,'Medicina Nuclear');
INSERT INTO Especialidade VALUES (38,'Medicina Preventiva e Social');
INSERT INTO Especialidade VALUES (39,'Nefrologia');
INSERT INTO Especialidade VALUES (40,'Neurocirurgia');
INSERT INTO Especialidade VALUES (41,'Neurologia');
INSERT INTO Especialidade VALUES (42,'Nutrologia');
INSERT INTO Especialidade VALUES (43,'Oftalmologia');
INSERT INTO Especialidade VALUES (44,'Oncologia Cl�nica');
INSERT INTO Especialidade VALUES (45,'Ortopedia e Traumatologia');
INSERT INTO Especialidade VALUES (46,'Otorrinolaringologia');
INSERT INTO Especialidade VALUES (47,'Patologia');
INSERT INTO Especialidade VALUES (48,'Patologia Cl�nica/Medicina Laboratorial');
INSERT INTO Especialidade VALUES (49,'Pediatria');
INSERT INTO Especialidade VALUES (50,'Pneumologia');
INSERT INTO Especialidade VALUES (51,'Psiquiatria');
INSERT INTO Especialidade VALUES (52,'Radiologia e Diagn�stico por Imagem');
INSERT INTO Especialidade VALUES (53,'Radioterapia');
INSERT INTO Especialidade VALUES (54,'Reumatologia');
INSERT INTO Especialidade VALUES (55,'Urologia');

GO

CREATE TABLE HospitalEspecialidade (
idHospital INT NOT NULL,
idEspecialidade INT NOT NULL,

CONSTRAINT HospitalEspecialidade_PK PRIMARY KEY (idHospital, idEspecialidade),
CONSTRAINT HospitalEspecialidade_FK1 FOREIGN KEY (idHospital) REFERENCES Hospital (idHospital),
CONSTRAINT HospitalEspecialidade_FK2 FOREIGN KEY (idEspecialidade) REFERENCES Especialidade (idEspecialidade)
)

GO

CREATE TABLE Associado (
idAssociado INT NOT NULL,
NomeAssociado VARCHAR(20) NOT NULL,
SobrenomeAssociado VARCHAR(30) NOT NULL,
CPF CHAR(14) NOT NULL,
DtNascimento DATE NOT NULL,
Sexo CHAR(1) NOT NULL,
DDDCelular CHAR(2) NOT NULL,
NroCelular CHAR(9) NOT NULL,
Email VARCHAR(50) NOT NULL,
  
CONSTRAINT Associado_PK PRIMARY KEY (idAssociado)
)

-- CRIAR INSERTS DE ASSOCIADO PARA SIMULAR

GO

CREATE TABLE IdentificacaoAtendimento (
idIdentificacaoAtendimento INT NOT NULL,
dsIdentificacaoAtendimento VARCHAR(10) NOT NULL,
dfIdentificacaoAtendimento VARCHAR(100) NULL,

CONSTRAINT IdentificacaoAtendimento_PK PRIMARY KEY (idIdentificacaoAtendimento)
)

INSERT INTO IdentificacaoAtendimento VALUES (1,'Azul','N�o Urgente, sem risco imediato de agravo � sa�de. Atendimento em at� 240 min.');   
INSERT INTO IdentificacaoAtendimento VALUES (2,'Verde','Pouco Urgente, baixo risco de agravo imediato � sa�de. Atendimento em at� 120 min.');  
INSERT INTO IdentificacaoAtendimento VALUES (3,'Amarelo','Urgente, condi��es que podem se agravar sem atendimento. Atendimento em at� 60 min.'); 
INSERT INTO IdentificacaoAtendimento VALUES (4,'Laranja','Muito Urgente, risco significativo de piora do quadro. Atendimento em at� 10 min.');  
INSERT INTO IdentificacaoAtendimento VALUES (5,'Vermelho','Emerg�ncia, risco imediato de perder a vida. Atendimento imediato.');

GO

CREATE TABLE StatusAtendimento (
idStatusAtendimento INT NOT NULL,
DsStatusAtendimento VARCHAR(25) NOT NULL,

CONSTRAINT StatusAtendimento_PK PRIMARY KEY (idStatusAtendimento)
)

INSERT INTO StatusAtendimento VALUES (1,'Iniciado');
INSERT INTO StatusAtendimento VALUES (2,'Finalizado'); 
INSERT INTO StatusAtendimento VALUES (3,'Cancelado');
INSERT INTO StatusAtendimento VALUES (4,'Suspenso');
INSERT INTO StatusAtendimento VALUES (5,'Em andamento');
INSERT INTO StatusAtendimento VALUES (6,'Aguardando'); 
INSERT INTO StatusAtendimento VALUES (7,'Encaminhado');
INSERT INTO StatusAtendimento VALUES (8,'Transferido'); 

GO

CREATE TABLE Evento (
idEvento INT NOT NULL,
dsEvento VARCHAR(25) NOT NULL,

CONSTRAINT Evento_PK PRIMARY KEY (idEvento)
)

INSERT INTO Evento VALUES (1,'Senha para Atendimento');
INSERT INTO Evento VALUES (2,'Triagem Diagn�stica');
INSERT INTO Evento VALUES (3,'Registro do Paciente'); 
INSERT INTO Evento VALUES (4,'Consulta M�dica');
INSERT INTO Evento VALUES (5,'Interna��o'); 
INSERT INTO Evento VALUES (6,'Exames Ambulatoriais'); 
INSERT INTO Evento VALUES (7,'Exames Cl�nicos'); 

CREATE TABLE Atendimentos (
idAtendimento INT NOT NULL,
idEspecialidade INT NOT NULL,
idStatusAtendimento INT NOT NULL,
idIdentificacaoAtendimento INT NOT NULL,
idHospital INT NOT NULL,
idAssociado INT NOT NULL,
dtAtendimento DATETIME NOT NULL,

CONSTRAINT Atendimentos_PK PRIMARY KEY (idAtendimento),
CONSTRAINT Atendimentos_FK1 FOREIGN KEY (idEspecialidade) REFERENCES Especialidade (idEspecialidade),
CONSTRAINT Atendimentos_FK2 FOREIGN KEY (idStatusAtendimento) REFERENCES StatusAtendimento (idStatusAtendimento),
CONSTRAINT Atendimentos_FK3 FOREIGN KEY (idIdentificacaoAtendimento) REFERENCES IdentificacaoAtendimento (idIdentificacaoAtendimento),
CONSTRAINT Atendimentos_FK5 FOREIGN KEY (idHospital) REFERENCES Hospital (idHospital),
CONSTRAINT Atendimentos_FK6 FOREIGN KEY (idAssociado) REFERENCES Associado (idAssociado)
)

-- CRIAR INSERTS DE ATENDIMENTOS PARA SIMULAR

GO

CREATE TABLE AtendimentosEventos (
idAtendimento INT NOT NULL,
idEvento INT NOT NULL,
acAtendimento CHAR(1) NOT NULL,
mtAtendimento DATETIME NOT NULL,

CONSTRAINT AtendimentosEventos_PK PRIMARY KEY (idAtendimento, idEvento, acAtendimento),
CONSTRAINT AtendimentosEventos_FK1 FOREIGN KEY (idAtendimento) REFERENCES Atendimentos (idAtendimento),
CONSTRAINT AtendimentosEventos_FK2 FOREIGN KEY (idEvento) REFERENCES Evento (idEvento),
)

GO

CREATE TABLE Usuario (
idUsuario INT NOT NULL IDENTITY,
idAssociado INT NOT NULL,
NomeUsuario VARCHAR(20) NOT NULL,
CPF CHAR(14) NULL,
Email VARCHAR(50) NOT NULL,
DtAcesso DATETIME NULL,
Latitude FLOAT NULL,
Longitude FLOAT NULL,
PasswordHash VARCHAR NULL,
PasswordSalt VARCHAR NULL,
TpUsuario VARCHAR(5) NOT NULL,
DtCadastro DATETIME NOT NULL,

CONSTRAINT Usuario_PK PRIMARY KEY (idUsuario),
CONSTRAINT Usuario_FK1 FOREIGN KEY (idAssociado) REFERENCES Associado (idAssociado)
)