# Escola API 🎓

Uma API RESTful moderna para gerenciamento de escolas, construída com **.NET 10** seguindo os melhores padrões de arquitetura e desenvolvimento.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=.net)
![C#](https://img.shields.io/badge/C%23-Latest-239120?style=flat-square&logo=csharp)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Latest-336791?style=flat-square&logo=postgresql)
![Docker](https://img.shields.io/badge/Docker-Latest-2496ED?style=flat-square&logo=docker)
![JWT](https://img.shields.io/badge/Auth-JWT-000000?style=flat-square)

---

## 📋 Sumário Executivo

A **Escola API** é uma solução enterprise-grade para gerenciar todas as operações de uma instituição educacional, incluindo:

- ✅ Gestão de cursos, turmas e disciplinas
- ✅ Controle de matrículas de alunos
- ✅ Registro e consulta de notas
- ✅ Autenticação segura com JWT
- ✅ Recuperação de senha
- ✅ Sistema de paginação para grandes volumes de dados
- ✅ Tratamento global de exceções
- ✅ Documentação automática com Swagger

---

## 🏗️ Arquitetura & Padrões

### 📐 Clean Architecture

O projeto segue rigorosamente os princípios da **Clean Architecture**, dividindo a solução em camadas bem definidas:

```
Escola.API (Apresentação)
    ↓
Escola.Application (Casos de Uso)
    ↓
Escola.Domain (Lógica de Negócio)
    ↓
Escola.Infra.Data (Persistência)
    ↓
Escola.Infra.IoC (Injeção de Dependência)
```

### 🎯 Padrões Implementados

| Padrão | Descrição | Localização |
|--------|-----------|-------------|
| **Repository Pattern** | Isolamento da lógica de acesso aos dados | `Escola.Infra.Data/Repositories` |
| **Dependency Injection** | IoC container para gerenciamento de dependências | `Escola.Infra.IoC` |
| **DTO Pattern** | Transferência de dados entre camadas | `Escola.Application/DTOs` |
| **Service Layer** | Encapsulamento da lógica de negócio | `Escola.Application/Services` |
| **Middleware** | Tratamento centralizado de exceções | `Escola.API/Middleware` |
| **FluentAPI/EF Core** | Configuração de entidades | `Escola.Infra.Data/EntitiesConfiguration` |

---

## 🛠️ Stack Tecnológico

### Backend
- **Framework**: ASP.NET Core 10.0
- **Linguagem**: C# 13
- **Banco de Dados**: PostgreSQL (com EF Core 10)
- **Autenticação**: JWT (JSON Web Tokens)
- **Documentação API**: Swagger/OpenAPI

### Segurança
- 🔐 Autenticação JWT tokenizada
- 🔐 Criptografia de senhas com HMACSHA512
- 🔐 Autorização baseada em roles (Admin/Aluno)
- 🔐 HTTPS obrigatório em produção

### Infraestrutura
- 📦 Docker & Docker Compose
- 🐘 PostgreSQL 14+
- 📝 EF Core Migrations
- 🚀 CI/CD Ready

---

## 📦 Estrutura de Projetos

### **Escola.Domain** 🧩
Core puro sem dependências externas. Contém:
- Entidades de negócio (Usuario, Curso, Turma, Matricula, Nota)
- Interfaces dos repositórios
- Lógica de domínio e validações
- Paginação

### **Escola.Application** 🔄
Casos de uso e orquestração:
- Services (regra de negócio)
- DTOs (Request/Response)
- Interfaces de services
- Exceções customizadas

### **Escola.Infra.Data** 💾
Persistência e acesso a dados:
- DbContext (EF Core)
- Repositories implementados
- Migrations
- Configurações de entidades
- Identity providers

### **Escola.Infra.IoC** 💉
Configuração de injeção de dependência:
- Setup de DbContext
- Registro de Services
- Configuração JWT
- Claims extraction

### **Escola.API** 🌐
Camada de apresentação:
- Controllers REST
- Middleware de exceções
- Modelos de request/response
- Swagger configuration
- Dockerfile para containerização

---

## 🚀 Features Principais

### 👤 Gerenciamento de Usuários
- Cadastro com perfis automáticos (Admin/Aluno)
- Autenticação segura with JWT
- Recuperação de senha com código temporário
- Alteração de senha
- Listagem paginada

### 📚 Gerenciamento Acadêmico
- **Cursos**: CRUD completo com status ativo/inativo
- **Turmas**: Associação com cursos e períodos
- **Matrículas**: Vínculo aluno-turma com histórico
- **Notas**: Registro e consulta de desempenho
- **Paginação**: Suporte completo em listagens

### 🔒 Segurança
- Autenticação JWT com issuer/audience validados
- Senhas criptografadas com HMACSHA512 + Salt
- Roles-based authorization (Administrador/Aluno)
- Middleware global para tratamento de erros
- Validação de entrada em DTOs

### 📊 API Modern
- RESTful principles
- Versionamento de API
- Pagination headers customizados
- Status codes apropriados
- Swagger UI interativa

---

## 🗄️ Banco de Dados

### Diagrama de Entidades

```
USUARIO
├── Perfil (Administrador/Aluno)
├── Email (único)
└── PasswordHash/Salt

CURSO
├── Nome
└── Ativo

TURMA
├── Codigo
├── Periodo
├── Curso FK
└── Ativo

MATRICULA
├── Usuario FK
├── Turma FK
└── DataMatricula

NOTA
├── Matricula FK
├── Valor
└── Disciplina

CODIGO_RECUPERACAO_SENHA
├── Usuario FK
├── Codigo
└── DataCriacao
```

### Migrations
O projeto inclui migrations versionadas:
- Initial migration com schema completo
- Campo `Ativo` em todas entidades
- Suporte a password recovery

---

## 📖 Como Usar

### Pré-requisitos
- .NET 10 SDK
- PostgreSQL 14+
- Docker & Docker Compose (opcional)

### 1️⃣ Clonar o Repositório
```bash
git clone https://github.com/Augusto-LJ/Escola.git
cd Escola
```

### 2️⃣ Configurar Variáveis de Ambiente
Edite `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=escoladb;Username=postgres;Password=sua_senha"
  },
  "Jwt": {
    "SecretKey": "sua_chave_secreta_muito_longa",
    "Issuer": "sua_aplicacao",
    "Audience": "seus_usuarios"
  }
}
```

### 3️⃣ Aplicar Migrations
```bash
dotnet ef database update --project Escola.Infra.Data
```

### 4️⃣ Executar a Aplicação
```bash
dotnet run --project Escola.API
```

Swagger estará disponível em: `http://localhost:5000/swagger`

### 🐳 Com Docker
```bash
docker build -f Escola.API/Dockerfile -t escola-api .
docker run -p 8080:8080 -e 'ConnectionStrings__DefaultConnection=...' escola-api
```

---

## 🧪 Endpoints Principais

### 🔑 Autenticação
```http
POST /api/usuario/login
Content-Type: application/json

{
  "email": "admin@escola.com",
  "senha": "segura123"
}
```

### 📚 Cursos
```http
GET    /api/curso                    # Listar com paginação
POST   /api/curso                    # Criar
PUT    /api/curso/{id}               # Atualizar
DELETE /api/curso/{id}               # Deletar
GET    /api/curso/{id}               # Detalhes
```

### 👥 Usuários
```http
GET    /api/usuario                  # Listar
POST   /api/usuario                  # Registrar
PUT    /api/usuario/{id}             # Atualizar
DELETE /api/usuario/{id}             # Deletar
POST   /api/usuario/alterar-senha    # Trocar senha
```

### 📖 Turmas
```http
GET    /api/turma                    # Listar
POST   /api/turma                    # Criar
PUT    /api/turma/{id}               # Atualizar
DELETE /api/turma/{id}               # Deletar
```

### 📝 Matrículas & Notas
```http
GET    /api/matricula                # Listar matrículas
POST   /api/matricula                # Matricular
GET    /api/nota                     # Listar notas
POST   /api/nota                     # Registrar nota
```

---

## 🔐 Autenticação & Autorização

### Fluxo JWT
1. Usuário faz login com email/senha
2. API valida credenciais e gera JWT token
3. Client armazena token
4. Requests subsequentes incluem: `Authorization: Bearer {token}`
5. API valida o token a cada request

### Roles
- **Administrador**: Acesso total + CRUD de todas entidades
- **Aluno**: Acesso limitado ao seu próprio perfil

```csharp
[Authorize(Roles = "Administrador")]
[HttpPost]
public async Task<IActionResult> CreateCurso(...)
```

---

## 🎯 Diferenciais Técnicos

### ✨ Boas Práticas
- ✅ **Null Safety**: Nullable reference types habilitados
- ✅ **Async/Await**: Toda operação I/O é assíncrona
- ✅ **User Secrets**: Senhas seguras em development
- ✅ **Global Exception Handling**: Erro padronizado
- ✅ **Paginação Eficiente**: Skip/Take com metadados

### 📊 Tratamento de Erros
```json
{
  "status": 400,
  "titulo": "Requisição Inválida",
  "mensagem": "Validação falhou",
  "erros": {
    "email": ["Email já cadastrado"]
  }
}
```

### 🔄 Paginação
```
GET /api/usuario?pageNumber=1&pageSize=10

Headers:
X-Pagination: {"pageNumber":1,"pageSize":10,"totalPages":5,"totalItems":50}

Response:
[
  { "id": 1, "nome": "Usuario 1" },
  { "id": 2, "nome": "Usuario 2" }
]
```

---

## 🚀 Futuras Melhorias

- [ ] Integração com fila de mensagens (RabbitMQ)
- [ ] Cache distribuído (Redis)
- [ ] Logging centralizado (Serilog + ELK)
- [ ] Unit & Integration Tests
- [ ] Soft deletes
- [ ] Auditoria completa
- [ ] GraphQL endpoint
- [ ] Mobile app complementar

---

## 🤝 Contribuindo

Pull requests são bem-vindos! Para mudanças maiores:
1. Faça fork do projeto
2. Crie sua feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.
