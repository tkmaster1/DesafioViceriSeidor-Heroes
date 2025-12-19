const msgToastComponent = {
    template: `<div class="toasts-container ">
                <div class="toast fade hide " data-autohide="false" id="toastSucesso">
                    <div class="toast-header bg-success text-white">
                        <i class="far fa-check-circle text-white me-2"></i>
                        <strong class="me-auto">Sucesso!</strong>	
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="toast"></button>
                    </div>
                    <div class="toast-body">
                        {{msgSucesso}}
                    </div>
                </div>
            </div>
            <div class="toasts-container ">
                <div class="toast fade hide " data-autohide="false" id="toastErro">
                    <div class="toast-header bg-danger text-white">
                        <i class="far fa-check-circle text-white me-2"></i>
                        <strong class="me-auto">Erro!</strong>			
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="toast"></button>
                    </div>
                    <div class="toast-body">
                        {{msgErro}}
                    </div>
                </div>
            </div>
`,
    props: ['msgSucesso', 'msgErro'],
    data: () => ({ item: 'test' }),
}
