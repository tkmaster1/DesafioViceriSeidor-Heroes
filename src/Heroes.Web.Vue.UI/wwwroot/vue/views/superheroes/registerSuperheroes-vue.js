createApp({
    data() {
        return {
            newSuperhero: {
                name: '',
                description: '',
                status: true
            },

            toastSucessoMensagem: "",
            toastMensagemErro: "",
            isInvalid: false,

            messageErrorValidationSuperhero: [],

            // Loading page
            isLoading: false,
            fullPage: true,
        }
    },
    computed: {
        classesErrorsValidation() {
            return {
                "was-validated": this.isInvalid
            }
        }
    },
    mounted() {
    },
    methods: {

        
    }
})
    .use(VueLoading.LoadingPlugin)
    .component('loading', VueLoading.Component)
    .mount('#mdCreateSuperhero')