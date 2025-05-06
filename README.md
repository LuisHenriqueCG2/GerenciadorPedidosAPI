# API para Gerenciamento de Pedidos

Este projeto é uma Web API desenvolvida em ASP.NET para o gerenciamento de pedidos de uma loja.  

Ele foi desenvolvido como parte de um desafio técnico proposto pela Inside Sistemas, no contexto de um processo seletivo para a vaga de Desenvolvedor Backend .NET.

---

## 🎯 Objetivo

Criar uma WebAPI que permita realizar o controle de pedidos e produtos, aplicando boas práticas de desenvolvimento, princípios de **Domain-Driven Design (DDD)**.

---

## ✅ Funcionalidades

A API permite:

- **Iniciar um novo pedido**: Cria um pedido com status de aberto.
- **Adicionar produtos ao pedido**: Adiciona produtos a um pedido específico que esteja aberto.
- **Remover produtos do pedido**: Remove produtos de um pedido aberto.
- **Fechar um pedido**: Muda o status do pedido para fechado, impedindo novas alterações, exceto excluí-lo.
- **Excluir um pedido**: Deleta um pedido da base de dados.
- **Listar pedidos**: Lista todos os pedidos, com suporte à paginação.
- **Filtrar pedidos por status**: Filtra pedidos pelo status (aberto, fechado ou cancelado).
- **Obter detalhes de um pedido**: Retorna os detalhes de um pedido e seus produtos associados.
- **Criar um novo produto**: Permite cadastrar um produto no sistema.
- **Listar produtos**: Lista os produtos cadastrados.

---

## ⚙️ Regras de Negócio

- Produtos **não podem ser adicionados ou removidos** de pedidos com status **fechado ou cancelado**.
- Um pedido **só pode ser fechado se tiver ao menos um produto**.
- O status de um pedido pode ser alterado para **cancelado**, mas o cancelamento **não remove os produtos associados**.

---

## 🗂️ Estrutura do Projeto

A API foi construída seguindo o conceito de **DDD**. Abaixo está um resumo das camadas:

- **Domain**: Responsável por conter as entidades principais do sistema, como `Pedido` e `Produto`, além das regras de negócio fundamentais.
- **Application**: Camada de aplicação que orquestra as regras de negócio, atuando como intermediária entre a camada de dados e a API. Nela também são definidos os DTOs (Data Transfer Objects) — objetos usados para transportar dados entre as camadas, permitindo controlar exatamente quais informações são recebidas ou enviadas.
- **Infrastructure (ou Infra)**: Contém a implementação dos repositórios responsáveis pelo acesso ao banco de dados, além da configuração dos serviços e da Injeção de Dependência do sistema.
- **API**: Exposição dos controladores responsáveis por mapear as rotas HTTP e permitir a interação externa com o sistema.
- **Tests**: Módulo dedicado à criação de testes automatizados. Neste projeto, são utilizados testes unitários com o framework xUnit para validar o comportamento das funcionalidades de forma isolada.

---

## 📌 Rotas da API

### 🛒 Pedido
URL BASE `api/v1/Pedidos`

- `POST /CriarPedido`  
  Inicia um novo pedido.

- `POST /AdicionarProduto`  
  Adiciona um produto a um pedido existente.

- `PUT /RemoverProduto`  
  Remove um produto de um pedido.

- `PUT /FecharPedido`  
  Fecha um pedido, impedindo novas alterações, exceto excluí-lo.

- `PUT /FaturarPedido`  
  Fatura um pedido, alterando seu status para faturado e incluído a data de faturamento.

- `DELETE /ExcluirPedido`  
  Deleta um pedido.

- `GET /ConsultarPedidoId`  
  Lista um Pedido pelo ID.

- `GET /ListarTodos`  
  Filtra os pedidos de acordo com o status (Aberto, Fechado, Cancelado/Exccluído e Faturado), além da listagem com paginação.


### 📦 Produto
URL BASE `api/v1/Produtos`

- `POST /Cadastrar`  
  Cadastrar um novo produto.
  
- `POST /Alterar`  
  Alterar um produto existente.

- `POST /Deletar`  
  Deletar um produto da base de dados.    

- `POST /ConsultarId`  
  Consulta um produto pelo ID.   

- `GET /ListarTodos`  
  Lista todos os produtos.


---

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- Injeção de Dependência
- DDD (Domain-Driven Design)
- Swagger para documentação
- xUnit para testes unitários

---

## 🚀 Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/LuisHenriqueCG2/GerenciadorPedidos.git

2. Configurar a string de conexão com o banco de dados:  

![image](https://github.com/user-attachments/assets/e21bced7-cc9f-4901-9f52-741505132798)     

- No campo `Server=` Informe o nome do seu servidor ou instância do SQL Server.
- 
![image](https://github.com/user-attachments/assets/3bd9e8b6-7b46-4950-bbb9-e292ca6d5848)

- No campo `Database=` informer o nome do seu banco de dados.
- `User Id=` e `Password=` será o usuário e senha de conexão do banco.

3. No Visual Studio, acesse o Console de Gerenciador de Pacotes, conforme demonstrado abaixo:

![image](https://github.com/user-attachments/assets/000aed8e-aaf9-400b-ba82-f0c7558ef856)

- No Console, selecione o projeto `GerenciadorPedidos.Infra.loc`:

![image](https://github.com/user-attachments/assets/1060eaca-ec8f-4dbc-8d7d-134f41ac77ab)


- Rode os comandos:
 ```bash
  dotnet ef migrations add NomeDaMigration
```
Isso irá criar as migrations das tabelas no banco de dados.  

Em seguida rode:  
```bash
 dotnet ef database update
```

Esse comando será para atualizar as tabelas conforme estruturado nas `EntitiesConfiguration` do projeto.  

Se der tudo certo, basta definir o projeto `GerenciadorPedidos.API` como projeto de incialização e em seguida executar.  

![image](https://github.com/user-attachments/assets/a35541c7-3c7c-40d1-b6fb-f06f34a1b166)  
  
![image](https://github.com/user-attachments/assets/2ac86727-7da2-487f-85a1-718be6e38c35)


