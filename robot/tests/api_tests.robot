*** Settings ***
Resource          resources/keywords.robot

*** Variables ***
${API_URL}        https://sanduba-costumer-function.azurewebsites.net/api/

*** Test Cases ***
Ao realizar um login com usuário e senha válidos
    [Documentation]  Cenário: Ao realizar um login com usuário e senha válidos
    Dado que eu tenho um usuario e senha validos
    Quanto realizar a requisição de login
    Então deve retornar status da resposta 200
