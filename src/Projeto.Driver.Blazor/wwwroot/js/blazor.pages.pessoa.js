MethodCstoJsCall = (nameEvent, codigo) => {
    console.log(`Evento ${nameEvent} : ${codigo}!`);
    DotNet.invokeMethodAsync('Projeto.Driver.Blazor', 'MethodJstoCsCall', nameEvent, codigo);
};

exibeMensagem = (mensagem) => {
    console.log(`${mensagem}!`);
}

closeModal = () => {
    $('#exampleModal').modal('hide')
    showMessage();
}

messageConfirma = (pessoa) => {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        //console.log(pessoa)
        //console.log(result)
        //return result.isConfirmed        
        if (result.isConfirmed) {
            console.log(pessoa);
            DotNet.invokeMethodAsync('Projeto.Driver.Blazor', 'ExclusaoConfirmada', pessoa);
            //Swal.fire(
            //    'Deleted!',
            //    'Your file has been deleted.',
            //    'success'
            //)
        }
    })
}