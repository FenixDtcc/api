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
INSERT INTO Logradouros VALUES (6,'Chacara');
INSERT INTO Logradouros VALUES (7,'Colonia');
INSERT INTO Logradouros VALUES (8,'Condominio');
INSERT INTO Logradouros VALUES (9,'Conjunto');
INSERT INTO Logradouros VALUES (10,'Distrito');
INSERT INTO Logradouros VALUES (11,'Esplanada');
INSERT INTO Logradouros VALUES (12,'Estacao');
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
INSERT INTO Logradouros VALUES (24,'Nucleo');
INSERT INTO Logradouros VALUES (25,'Parque');
INSERT INTO Logradouros VALUES (26,'Passarela');
INSERT INTO Logradouros VALUES (27,'Patio');
INSERT INTO Logradouros VALUES (28,'Praca');
INSERT INTO Logradouros VALUES (29,'Quadra');
INSERT INTO Logradouros VALUES (30,'Recanto');
INSERT INTO Logradouros VALUES (31,'Residencial');
INSERT INTO Logradouros VALUES (32,'Rodovia');
INSERT INTO Logradouros VALUES (33,'Rua');
INSERT INTO Logradouros VALUES (34,'Setor');
INSERT INTO Logradouros VALUES (35,'Sitio');
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
Cnpj CHAR(18) NOT NULL,
RazaoSocial VARCHAR(100) NOT NULL,
NomeFantasia VARCHAR(50) NOT NULL,
idLogradouro INT NOT NULL,
Endereco VARCHAR(50) NOT NULL,
Numero VARCHAR(10) NOT NULL,
Complemento VARCHAR(15) NULL,
Bairro VARCHAR(25) NOT NULL,
Cidade VARCHAR(25) NOT NULL,
Uf CHAR(2) NOT NULL,
Cep CHAR(9) NOT NULL,
Latitude FLOAT NOT NULL,
Longitude FLOAT NOT NULL,

CONSTRAINT Hospital_PK PRIMARY KEY (idHospital),
CONSTRAINT Hospital_FK1 FOREIGN KEY (idLogradouro) REFERENCES Logradouros (idLogradouro)
)

INSERT INTO Hospital VALUES (1,'84.946.165/0001-40', 'Hospital e Maternidade A LTDA', 'Hospital A', 
33, 'Dr. Edson de Melo', '357', NULL, 'Vila Maria', 'Sao Paulo', 'SP', '02122-080', 
-23.511509793821133, -46.583786716042916);
INSERT INTO Hospital VALUES (2,'85.182.404/0001-04', 'Hospital e Maternidade B LTDA', 'Hospital B', 
33, 'Voluntarios da Patria', '2786', NULL, 'Santana', 'Sao Paulo', 'SP', '02402-100', 
-23.495594010219794, -46.625444342874154);
INSERT INTO Hospital VALUES (3,'17.035.551/0001-93', 'Hospital e Maternidade C LTDA', 'Hospital C', 
33, 'Voluntarios da Patria', '3693', NULL, 'Santana', 'Sao Paulo', 'SP', '02401-200', 
-23.487175659964617, -46.62721324059225);
INSERT INTO Hospital VALUES (4,'88.466.041/0001-19', 'Hospital e Maternidade D LTDA', 'Hospital D', 
4, 'Nova Cantareira', '2398', NULL, 'Agua Fria', 'Sao Paulo', 'SP', '02340-000', 
-23.476232382968988, -46.61079992842134);
INSERT INTO Hospital VALUES (5,'64.507.701/0001-02', 'Hospital e Maternidade E LTDA', 'Hospital E', 
33, 'Pistoia', '100', NULL, 'Parque Novo Mundo', 'Sao Paulo', 'SP', '02189-000', 
-23.506910871125083, -46.56865477021042);

GO

CREATE TABLE Contatos (
idHospital INT NOT NULL,
idContato INT NOT NULL,
idTipoContato INT NOT NULL,
dsContato VARCHAR(50) NOT NULL,
infoContato VARCHAR(100) NULL,

CONSTRAINT Contatos_PK PRIMARY KEY (idHospital, idContato, idTipoContato),
CONSTRAINT Contatos_FK1 FOREIGN KEY (idHospital) REFERENCES Hospital (idHospital),
CONSTRAINT Contatos_FK2 FOREIGN KEY (idTipoContato) REFERENCES TiposContato (idTipoContato),
)

INSERT INTO Contatos VALUES (1, 1, 1, '(11) 3758-5202', NULL);
INSERT INTO Contatos VALUES (1, 2, 2, 'atendimento@hospitala.com.br', NULL);
INSERT INTO Contatos VALUES (2, 3, 1, '(11) 3784-9463', NULL);
INSERT INTO Contatos VALUES (2, 4, 2, 'atendimento@hospitalb.com.br', NULL);
INSERT INTO Contatos VALUES (2, 5, 3, '(11) 98182-4538', NULL);
INSERT INTO Contatos VALUES (3, 6, 1, '(11) 3642-2653', NULL);
INSERT INTO Contatos VALUES (3, 7, 2, 'atendimento@hospitalc.com.br', NULL);
INSERT INTO Contatos VALUES (4, 8, 1, '(11) 3949-8281', NULL);
INSERT INTO Contatos VALUES (4, 9, 2, 'atendimento@hospitald.com.br', NULL);
INSERT INTO Contatos VALUES (5, 10, 1, '(11) 2889-5919', NULL);
INSERT INTO Contatos VALUES (5, 11, 2, 'atendimento@hospitale.com.br', NULL);
INSERT INTO Contatos VALUES (5, 12, 3, '(11) 99612-5577', NULL);

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
INSERT INTO Especialidade VALUES (7,'Cirurgia de Mao');
INSERT INTO Especialidade VALUES (8,'Cirurgia de Cabeca e Pescoco');
INSERT INTO Especialidade VALUES (9,'Cirurgia do Aparelho Digestivo');
INSERT INTO Especialidade VALUES (10,'Cirurgia Geral');
INSERT INTO Especialidade VALUES (11,'Cirurgia Oncologica');
INSERT INTO Especialidade VALUES (12,'Cirurgia Pediatrica');
INSERT INTO Especialidade VALUES (13,'Cirurgia Plastica');
INSERT INTO Especialidade VALUES (14,'Cirurgia Toracica');
INSERT INTO Especialidade VALUES (15,'Cirurgia Vascular');
INSERT INTO Especialidade VALUES (16,'Clinica Medica');
INSERT INTO Especialidade VALUES (17,'Coloproctologia');
INSERT INTO Especialidade VALUES (18,'Dermatologia');
INSERT INTO Especialidade VALUES (19,'Endocrinologia e Metabologia');
INSERT INTO Especialidade VALUES (20,'Endoscopia');
INSERT INTO Especialidade VALUES (21,'Gastroenterologia');
INSERT INTO Especialidade VALUES (22,'Genetica Medica');
INSERT INTO Especialidade VALUES (23,'Geriatria');
INSERT INTO Especialidade VALUES (24,'Ginecologia e Obstetricia');
INSERT INTO Especialidade VALUES (25,'Hematologia e Hemoterapia');
INSERT INTO Especialidade VALUES (26,'Homeopatia');
INSERT INTO Especialidade VALUES (27,'Infectologia');
INSERT INTO Especialidade VALUES (28,'Mastologia');
INSERT INTO Especialidade VALUES (29,'Medicina de Emergencia');
INSERT INTO Especialidade VALUES (30,'Medicina de Familia e Comunidade');
INSERT INTO Especialidade VALUES (31,'Medicina do Trabalho');
INSERT INTO Especialidade VALUES (32,'Medicina de Trafego');
INSERT INTO Especialidade VALUES (33,'Medicina Esportiva');
INSERT INTO Especialidade VALUES (34,'Medicina Fisica e Reabilitacao');
INSERT INTO Especialidade VALUES (35,'Medicina Intensiva');
INSERT INTO Especialidade VALUES (36,'Medicina Legal e Pericia Medica');
INSERT INTO Especialidade VALUES (37,'Medicina Nuclear');
INSERT INTO Especialidade VALUES (38,'Medicina Preventiva e Social');
INSERT INTO Especialidade VALUES (39,'Nefrologia');
INSERT INTO Especialidade VALUES (40,'Neurocirurgia');
INSERT INTO Especialidade VALUES (41,'Neurologia');
INSERT INTO Especialidade VALUES (42,'Nutrologia');
INSERT INTO Especialidade VALUES (43,'Oftalmologia');
INSERT INTO Especialidade VALUES (44,'Oncologia Clinica');
INSERT INTO Especialidade VALUES (45,'Ortopedia e Traumatologia');
INSERT INTO Especialidade VALUES (46,'Otorrinolaringologia');
INSERT INTO Especialidade VALUES (47,'Patologia');
INSERT INTO Especialidade VALUES (48,'Patologia Clinica/Medicina Laboratorial');
INSERT INTO Especialidade VALUES (49,'Pediatria');
INSERT INTO Especialidade VALUES (50,'Pneumologia');
INSERT INTO Especialidade VALUES (51,'Psiquiatria');
INSERT INTO Especialidade VALUES (52,'Radiologia e Diagnostico por Imagem');
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

INSERT INTO HospitalEspecialidade VALUES (1, 16);
INSERT INTO HospitalEspecialidade VALUES (1, 45);
INSERT INTO HospitalEspecialidade VALUES (1, 49);
INSERT INTO HospitalEspecialidade VALUES (2, 16);
INSERT INTO HospitalEspecialidade VALUES (2, 45);
INSERT INTO HospitalEspecialidade VALUES (2, 49);
INSERT INTO HospitalEspecialidade VALUES (3, 16);
INSERT INTO HospitalEspecialidade VALUES (3, 45);
INSERT INTO HospitalEspecialidade VALUES (3, 49);
INSERT INTO HospitalEspecialidade VALUES (4, 16);
INSERT INTO HospitalEspecialidade VALUES (4, 45);
INSERT INTO HospitalEspecialidade VALUES (4, 49);
INSERT INTO HospitalEspecialidade VALUES (5, 16);
INSERT INTO HospitalEspecialidade VALUES (5, 45);
INSERT INTO HospitalEspecialidade VALUES (5, 49);

GO

CREATE TABLE Associado (
idAssociado INT NOT NULL,
NomeAssociado VARCHAR(20) NOT NULL,
SobrenomeAssociado VARCHAR(30) NOT NULL,
Cpf CHAR(14) NOT NULL,
Sexo CHAR(1) NOT NULL,
DddCelular CHAR(2) NOT NULL,
NroCelular CHAR(9) NOT NULL,
Email VARCHAR(50) NOT NULL,

CONSTRAINT Associado_PK PRIMARY KEY (idAssociado)
)

INSERT INTO Associado VALUES (1, 'Aparecida', 'Fabiana Lopes', '870.853.739-97', 'F', '11', '999282363', 'aparecida_fabiana_lopes@gmail.com');
INSERT INTO Associado VALUES (2, 'Osvaldo', 'Diego Dias', '761.763.293-29', 'M', '11', '991879663', 'osvaldo_dias@outlook.com');

GO

CREATE TABLE IdentificacaoAtendimento (
idIdentificacaoAtendimento INT NOT NULL,
dsIdentificacaoAtendimento VARCHAR(10) NOT NULL,
dfIdentificacaoAtendimento VARCHAR(100) NULL,

CONSTRAINT IdentificacaoAtendimento_PK PRIMARY KEY (idIdentificacaoAtendimento)
)

INSERT INTO IdentificacaoAtendimento VALUES (1,'Azul','Nao Urgente, sem risco imediato de agravo a saude. Atendimento em ate 240 min.');   
INSERT INTO IdentificacaoAtendimento VALUES (2,'Verde','Pouco Urgente, baixo risco de agravo imediato a saude. Atendimento em ate 120 min.');  
INSERT INTO IdentificacaoAtendimento VALUES (3,'Amarelo','Urgente, condicoes que podem se agravar sem atendimento. Atendimento em ate 60 min.'); 
INSERT INTO IdentificacaoAtendimento VALUES (4,'Laranja','Muito Urgente, risco significativo de piora do quadro. Atendimento em ate 10 min.');  
INSERT INTO IdentificacaoAtendimento VALUES (5,'Vermelho','Emergencia, risco imediato de perder a vida. Atendimento imediato.');

GO

CREATE TABLE StatusAtendimento (
idStatusAtendimento INT NOT NULL,
DsStatusAtendimento VARCHAR(25) NOT NULL,

CONSTRAINT StatusAtendimento_PK PRIMARY KEY (idStatusAtendimento)
)

INSERT INTO StatusAtendimento VALUES (1,'Iniciado');
INSERT INTO StatusAtendimento VALUES (2,'Aguardando'); 
INSERT INTO StatusAtendimento VALUES (3,'Em andamento');
INSERT INTO StatusAtendimento VALUES (4,'Finalizado'); 
INSERT INTO StatusAtendimento VALUES (5,'Cancelado');
INSERT INTO StatusAtendimento VALUES (6,'Suspenso');
INSERT INTO StatusAtendimento VALUES (7,'Encaminhado');
INSERT INTO StatusAtendimento VALUES (8,'Transferido'); 
GO


CREATE TABLE Evento (
idEvento INT NOT NULL,
dsEvento VARCHAR(25) NOT NULL,

CONSTRAINT Evento_PK PRIMARY KEY (idEvento)
)

INSERT INTO Evento VALUES (1,'Senha para Atendimento');
INSERT INTO Evento VALUES (2,'Triagem Diagnostica');
INSERT INTO Evento VALUES (3,'Registro do Paciente'); 
INSERT INTO Evento VALUES (4,'Consulta Medica');
INSERT INTO Evento VALUES (5,'Internacao'); 
INSERT INTO Evento VALUES (6,'Exames Ambulatoriais'); 
INSERT INTO Evento VALUES (7,'Exames Clinicos'); 

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

-- CRIAR INSERTS DE ATENDIMENTOS PARA SIMULAR

GO

CREATE TABLE Usuario (
idUsuario INT NOT NULL IDENTITY (1,1),
idAssociado INT NULL,
NomeUsuario VARCHAR(20) NOT NULL,
Cpf CHAR(14) NOT NULL,
Email VARCHAR(50) NOT NULL,
DtAcesso DATETIME NULL,
Latitude FLOAT NULL,
Longitude FLOAT NULL,
PasswordHash VARBINARY(MAX) NULL,
PasswordSalt VARBINARY(MAX) NULL,
TpUsuario VARCHAR(13) NOT NULL,
DtCadastro DATETIME NULL,

CONSTRAINT Usuario_PK PRIMARY KEY (idUsuario),
CONSTRAINT Usuario_FK1 FOREIGN KEY (idAssociado) REFERENCES Associado (idAssociado)
)