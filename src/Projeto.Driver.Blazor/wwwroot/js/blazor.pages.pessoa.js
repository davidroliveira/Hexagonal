MethodCstoJsCall = (nameEvent, codigo) => {
    console.log(`Evento ${nameEvent} : ${codigo}!`);
    DotNet.invokeMethodAsync('Projeto.Driver.Blazor', 'MethodJstoCsCall', nameEvent, codigo);
};

exibeMensagem = (mensagem) => {
    console.log(`${mensagem}!`);
}

closeModal = () => {
    $('#exampleModal').modal('hide')
}