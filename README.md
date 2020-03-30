# PedidoAPI

api de pedido para um sorveteria para ser utilizada como um frente de caixa, essa api foi desenvolvida utilizando o Asp.Net Core 2.1 junto com o entity framewor com suporte para docker e rodar a mesma em um container Linux.

foi criado varias requisição para incluir, alterar, pesquisar(todas ou por ID) e excluir, todas essa requisição são para sabor e tamanhos
ja para pedidos foi seguida outra  abordagem, onde foi desenvolvido outras requisições sendo elas:

### metodo Get para sabores
localhost/api/sabor, vai listar todos os sabores com as combinações disponiveis para todos os tamanhos disponiveis.
Exemplo para a resposta do sabor morango : obs não existo só o sabor morango
{
    "codigoSabor": 1,
    "sabor": "Morango",
    "descricaoSabor": "Sorvete de Açai Sabor Morango",
    "codigoEmba": 1,
    "nomeEmbalagem": "Pequeno",
    "descricaoEmbalagem": "Copo Pequeno de 300 ML",
    "espera": 5,
    "valor": 10.0
  },
  {
    "codigoSabor": 1,
    "sabor": "Morango",
    "descricaoSabor": "Sorvete de Açai Sabor Morango",
    "codigoEmba": 2,
    "nomeEmbalagem": "Medio",
    "descricaoEmbalagem": "Copo Medio de 500 ML",
    "espera": 7,
    "valor": 13.0
  },
  {
    "codigoSabor": 1,
    "sabor": "Morango",
    "descricaoSabor": "Sorvete de Açai Sabor Morango",
    "codigoEmba": 3,
    "nomeEmbalagem": "Grande",
    "descricaoEmbalagem": "Copo Grande de 700 ML",
    "espera": 10,
    "valor": 15.0
  }

### Post Pedido
localhos/api/pedido, essa requisição Post do http é utilizada para criar um pedido, passando os parametro por json no corpo da requisição, segue um exemplo:
{
  "tamanhoId": 3,
  "saborID": 3,
  "finalizado": false  
}
#### OBSnão é informado o id devido a o banco que foi utilizado oferecendo o auto incremento para o ID, ja dos sabor e tamanho é obrigatorio

### get para pegar o pedido especifico/todos: 
localhost/api/Pedido/ID, esse é a requisição utiliaza para listar um pedido pedidos em especifico, caso queira listar todos retire o "/ID" da requisição que a API o atenderá
exemplo de requisição Json:
{
  "id": 4
}

#### OBS: quando for fazer uma requisição passando o ID especifico na URL da chamada passar o mesmo Código que está no corpo da requisição  no formato Json

### PUT para alterar os Adcionais do Pedido
LocalHost/api/PEDIDO/10, essa será uma requisição para alterar o pedido e incluir Opcionais  no pedido , para o mesmo basta adcionar os id's dos Opcionais
{
  "id": 10,
  "opcional": 
  [
    {
      "pedidoId": 10,
      "opcionaisId": 2
      }
    }
  ]
} 
#### OBS: caso não saiba qual os id dos opcionais, basta solicitar ele pelo metodo get na URL "localhos/api/opcionais" que será listado  todos os opcionais cadastrado, caso queira ver um em expecifico basta adicionar o  ID "/10"

### PITCH esse requisição foi deixada por ultimo, por ela finalizar o pedido
 essa requisição foi idealizada para ser a ultima a ser selecionada, o seu objetivo é de finalizar o pedido e fazendo a somatoria de tempo e valores do pedido vai levar para ficar pronto, será somando o tempo adcional de opcionais caso selecionados e valores, como tambem contabilizado os mesmo pelo tamaho da embalagem, ja que cada embalagem possui um valor e tempo de preparo diferente, para finalizar o pedido basta enviar o id do mesmo na url e no corpo da requisição, caso seja diferente esse dois valores o mesmo não finalizar o pedido, segue o exemplo da requisição:
localhost/api/pedido/10
{
  "id": 10  
}
#### OBS o mesmo irar retornar um resumo com todos os item do pedido em formato Json, segue o exemplo abaixo 

{
  "id": 10,
  "tamanhoId": 3,
  "tamanho": {
    "id": 3,
    "nome": "Grande",
    "descricao": "Copo Grande de 700 ML",
    "tempoPreparo": 10,
    "valor": 15.0
  },
  "saborID": 3,
  "sabor": {
    "id": 3,
    "nome": "Kiwi",
    "descricao": "Sorvete de Açai  Com Kiwi",
    "addTempo": 5
  },
  "valorFinal": 15.0,
  "finalizado": true,
  "tempoEspera": 15,
  "opcional": []
}
"esse pedido não teve opcionais"








