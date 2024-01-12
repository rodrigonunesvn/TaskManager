# Task Manager

## Tecnologias Utilizadas

- .NET Core: Para o backend da aplicação.
- SQL Server: Como sistema de gerenciamento de banco de dados.
- Docker: Para contêinerização e fácil implantação.

## Instruções de Instalação e Configuração
Antes de começar, certifique-se de ter o Docker instalado em sua máquina. Clone o repositório para o seu ambiente local usando o Git.

## Como executar a aplicação

Navegue até a pasta raiz do projeto e execute o comando

````bash
docker-compose up --build
````

Isso iniciará a API e o banco de dados SQL Server. 

## Uso da API

Acesse http://localhost:8080/swagger para visualizar a documentação da API e testar as diferentes rotas e métodos disponíveis.

## Estrutura do Projeto

- TaskManager.API: Contém os controladores e a configuração da API.
- TaskManager.Application: Camada de serviço que contém a lógica de negócios.
- TaskManager.Domain: Define os modelos e interfaces usados na aplicação.
- TaskManager.Infrastructure: Implementa a persistência de dados.
- TaskManager.Persistence: Configurações de migração e contexto de banco de dados.
- TaskManager.Testes: Projeto para testes unitários que validam as regras de negócio.

## Fase 2: O que perguntar para o Product Owner?

1. Quais são as funcionalidades mais críticas que devemos priorizar na próxima fase?

2. Existem funcionalidades atuais que precisam ser melhoradas antes de introduzir novas?

3. Quais são os pontos de dor ou dificuldades enfrentadas pelos usuários na versão atual?

4. Existem ajustes na interface do usuário ou na experiência do usuário que devemos implementar?

5. Quais são os diferentes níveis de acesso e permissões necessários para diferentes tipos de usuários?

7. Há necessidade de relatórios ou análises adicionais?

8. Existem prazos críticos ou marcos importantes que precisamos considerar?

## Fase 3: Quais melhorias podemos considerar?

Relação de sugestões focadas em qualidade de código, arquitetura, performance e escalabilidade:

1. Refatoração e Melhorias de Código

Padrões de Código: Revisar e aplicar padrões de codificação consistentes para melhorar a legibilidade e a manutenção.

Princípios SOLID: Garantir que os princípios SOLID estejam sendo seguidos para promover um design de software robusto e manutenível.

Otimização de Consultas de Banco de Dados: Analisar e otimizar consultas para melhor desempenho, especialmente aquelas que são executadas frequentemente ou lidam com grandes volumes de dados.

2. Arquitetura e Design

Se a complexidade do sistema está crescendo, considerar a adoção de uma arquitetura de microserviços para melhor escalabilidade e manutenção.

API Gateway: Se estiver movendo para microserviços, considerar a implementação de um API Gateway para gerenciar solicitações e respostas.

3. Segurança e Autenticação

Melhorias na Autenticação e Autorização: Implementar um sistema de autenticação mais robusto (como OAuth2 ou OpenID Connect) e melhorar o controle de acesso baseado em funções.

Auditoria e Logging: Aprimorar os sistemas de auditoria e logging para monitoramento e diagnóstico.

4. Testes e Qualidade

Testes Automatizados: Aumentar a cobertura de testes automatizados, incluindo testes de integração e testes de carga.

Integração Contínua/Entrega Contínua (CI/CD): Fortalecer o pipeline de CI/CD para automatizar testes e implantações.

5. Performance e Escalabilidade

Caching: Implementar caching onde apropriado para melhorar a performance.

Balanceamento de Carga: Utilizar balanceadores de carga para distribuir o tráfego uniformemente em ambientes de alta disponibilidade.

Monitoramento e Resiliência: Implementar monitoramento abrangente e estratégias de resiliência para garantir a alta disponibilidade do sistema.

Essas melhorias visam não apenas aprimorar a funcionalidade e a performance do sistema, mas também garantir que a aplicação seja segura, escalável e fácil de manter a longo prazo.



