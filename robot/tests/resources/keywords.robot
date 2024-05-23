*** Settings ***
Library     RequestsLibrary
Library     OperatingSystem
Library     Collections

*** Variables ***
${API_URL}        https://sanduba-costumer-function.azurewebsites.net/api/
${VALID_LOGIN_PAYLOAD}    ${CURDIR}/../payloads/valid_login.json

*** Keywords ***
Dado que eu tenho um usuario e senha validos
    [Documentation]  Define a URL da API que será testada
    ${payload}=    Get File    ${VALID_LOGIN_PAYLOAD}
    Log    Payload: ${payload}
    Set Suite Variable  ${payload}

Quanto realizar a requisição de login
    [Documentation]  Realiza uma requisição GET para a API
    Create Session    mysession    ${API_URL}
    ${response}=    POST On Session    mysession    LoginUser    ${payload}
    Log    Status Code: ${response.status_code}
    Log    Response Content: ${response.content}
    Set Suite Variable  ${response}

Então deve retornar status da resposta 200
    [Documentation]  Verifica se o status da resposta é 200
    Should Be Equal As Numbers    ${response.status_code}    200