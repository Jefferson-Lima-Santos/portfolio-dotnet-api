# 📦 Portfolio .NET API

API desenvolvida em .NET 8 como parte do meu portfólio pessoal, com foco em arquitetura limpa, CI/CD, boas práticas de desenvolvimento e integração com serviços da Azure.

Este projeto foi idealizado para praticar, consolidar conhecimentos e servir de base para futuras implementações mais complexas.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/)
- MediatR (Mediator Pattern)
- Entity Framework Core
- SQL Server (Azure)
- GitHub Actions (CI/CD)
- Azure Web App
- Autenticação via API Key

---

## 🧱 Estrutura do Projeto

A arquitetura foi separada em três camadas principais:

- `Services`: Camada principal da aplicação, que orquestra a lógica e as chamadas entre as demais camadas.
- `Domain`: Contém as entidades e regras de negócio puras.
- `Infra`: Responsável pela persistência de dados, implementação de repositórios e configurações de banco.

---

## 🔒 Autenticação

A API exige uma **API Key** para acesso aos endpoints.  
A chave deve ser enviada no header da requisição (x-api-key)

## 🧼 Soft Delete

Foi implementado o padrão **Soft Delete** nas entidades do banco.  
Ao invés de remover fisicamente os dados, eles são apenas marcados como excluídos (`IsDeleted = true`), permitindo:

- Preservação de histórico
- Mais segurança e rastreabilidade
- Possibilidade de "restauração" futura

---

## ⚙️ CI/CD

O processo de deploy está totalmente automatizado com **GitHub Actions**:

- A cada `push` na branch principal, a pipeline roda automaticamente
- Faz o build e o deploy no **WebApp da Azure**
- Permite entregas contínuas e rastreáveis

---

## 🗃️ Banco de Dados

- Banco SQL hospedado na **Azure**
- **Conexão protegida** — apenas a API tem acesso
- **Schema criado via código** (EF Core Migrations)
- Todas as tabelas possuem **índices otimizados**, mesmo usando `Guid` como chave primária, garantindo performance nas consultas

