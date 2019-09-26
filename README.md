# Desafio

## Como executar
Clone o Projeto para um diretório qualquer em seu computador

```bash
git clone https://github.com/wendersontc/engenhos-devops.git
```

Depois navegue até o diretório criado
```bash
cd engenhos-devops
```

Para alterar dados parametrizados - arquivo appsettings.json dentro de ConsoleEngenhoDevops
```bash
{
    "ServiceConfigurations": {
      "ConnectionString": "mongodb://db",
      "Intervalo": 30000,
      "Url": "https://dev.azure.com/wendersontc",
      "Project" : "MyFirstProject",
      "BasicToken" : "ipzcsz6ljzu5jke7kbcayysrlk23mr4rkwxd5rbh73qlq3y2gzia",
      "DatabaseName": "WorkItemDb",
      "WorkItensCollectionName": "WorkItens"
    }
  }
```

### Com Docker
É necessário instalar em sua máquina:
- [Docker](https://www.docker.com/) 

É necessário instalar em sua máquina
- [Docker Compose](https://docs.docker.com/compose/install/)

Execute o comando:
```bash
docker-compose up -d
```

## Como acessar
Uma vez que o projeo esteja rodando corretamente, abra seu navegador e acesso a url
```
http://localhost:5000
```

A página principal da aplicação contendo os itens que foram inseridos serão apresentados

