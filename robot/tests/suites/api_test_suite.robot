*** Settings ***
Resource          ../resources/keywords.robot
Resource          ../api_tests.robot

*** Test Cases ***
Ao realizar um login com usuário e senha válidos
    [Documentation]  Ao realizar um login com usuário e senha válidos
    Dado que eu tenho um usuario e senha validos
    Quanto realizar a requisição de login
    Então deve retornar status da resposta 200
