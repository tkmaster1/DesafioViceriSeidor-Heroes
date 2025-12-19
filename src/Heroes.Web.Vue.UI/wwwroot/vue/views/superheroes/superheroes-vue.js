const { createApp, ref } = Vue

const superheroesApp = createApp({
    data() {
        return {
            superheroesResponse: {},

            // -- Inicio Filtros --

            filterCode: '',
            filterName: '',
            filterHeroName: '',
            // textStatusButton: "Status",
            statusSelected: 0,

            statusFilter: undefined,
            filterNameClicked: false,
            filterHeroNameClicked: false,
            filterCodeClicked: false,

            // -- Fim Filtros --

            //#region Métodos de Edição -- Inicio Edição --

            editionSuperhero: {
                name: '',
                heroName: '',
                birthDate: '',
                height: '',
                weight: '',
                status: true
            },
            isInvalidEditar: false,
            statusChecked: false,
            messageErrorValidationSuperheroEdit: [],

            // #endregion -- Fim Edição --

            // -- Inicio Paginação  --

            superheroesResponsePagination: {},
            responsePagination: {},
            isLoading: false,
            fullPage: true,
            totalPages: 1,
            maxPages: 6,

            // -- Fim Paginação  --
        }
    },
    computed: {
        classesErrorsValidationSuperheroesEdit() {
            return {
                "was-validated": this.isInvalidEditar
            }
        },
        statusBooleanToIntEdit() {
            return this.statusChecked ? true : false;
        }
    },
    mounted() {
        this.listSuperheroes(1)
    },
    methods: {

        //#region Filtros e Listagem - Index e Detalhe

        async filterNameOrHeroNameSuperheroes(filterClicked, valueFieldFilter) {
            if (filterClicked == 1) {
                this.filterNameClicked = true
                this.filterName = valueFieldFilter
            }
            else if (filterClicked == 2) {
                this.filterHeroNameClicked = true
                this.filterHeroName = valueFieldFilter
            } else {
                this.filterCodeClicked = true
                this.filterCode = valueFieldFilter
            }

            this.listSuperheroes(null)
        },

        async clearFilterSuperheroes(values) {
            this.textStatusButton = "Status"

            this.filterCode = ''
            this.filterName = ''
            this.filterHeroName = ''

            this.filterNameClicked = false
            this.filterHeroNameClicked = false
            this.filterCodeClicked = false

            this.statusSelected = 0
            this.statusFilter = undefined

            this.listSuperheroes()
        },

        async listSuperheroes(paginaAtual, statusFilter) {
            this.isLoading = true;

            let superheroesFilterMapper = new Object({
                code: this.filterCodeClicked == true ? this.filterCode : null,
                name: this.filterNameClicked == true ? this.filterName : '',
                heroName: this.filterHeroNameClicked == true ? this.filterHeroName : '',
                status: statusFilter == undefined ? this.statusSelected : statusFilter,
                currentPage: paginaAtual == null ? 1 : paginaAtual,
                pageSize: this.maxPages,
                orderBy: "name",
                sortBy: "asc",
            })

            try {
                const superheroesFilter = superheroesFilterMapper

                const response = await fetchData.fetchPostJson(
                    "/Superheroes/ListSuperheroesByFilters", superheroesFilter
                )
                //  console.log(response);
                this.superheroesResponsePagination = response

                this.superheroesResponse = response.data.result
                // console.log(this.superheroesResponse);

                this.isLoading = false
            } catch (error) {
                this.isLoading = false

                console.log(error);

                $('#toastErro').toast("show")
            }
        },

        // #endregion

        //#region Métodos de Edição

        async displayModalChangeSuperhero(code) {
            const response = await fetchData.fetchGetJson("/Superheroes/GetByCode/" + `${code}`)

            this.editionSuperhero = response.data

           // this.editionSuperhero.status == true ? this.statusChecked = true : this.statusChecked = false

            this.isInvalidEditar = false
        },

        clearFieldsChangeSuperhero() {
            this.editionSuperhero = {
                name: '',
                heroName: '',
                birthDate: '',
                height: '',
                weight: '',
                status: true
            }

            this.messageErrorValidationSuperheroEdit = []
            this.isInvalidEditar = false
        },

        // #endregion

        // #region Métodos de Exclusão

        async displayModalDeleteSuperhero(code) {

            try {
                const response = await fetchData.fetchGetJson(
                    "/Superheroes/GetByCode/" + `${code}`)

                this.editionSuperhero = response.data
                // console.log(response)
            }
            catch (error) {
                console.log(error)

                this.toastMensagemErro = "Ocorreu um erro ao tentar exibir a tela de excluir."

                $("#toastErro").toast("show")
            }
        },

        async saveRemoveSuperhero(code) {
            this.isLoading = true

            try {
                const response = await fetchData.fetchGetJson("/Superheroes/SaveRemoveSuperhero/" + `${code}`)

                if (!response.success) {
                    this.isLoading = false

                    this.fecharModal("Excluir")

                    $("#toastErro").toast("show")

                } else {
                    this.isLoading = false

                    console.log('Enviado com sucesso!')

                    Swal.fire(
                        'Enviado com sucesso!',
                        'Super-herói excluído com sucesso.',
                        'success'
                    ).then((result) => {
                        this.fecharModal("Excluir")

                        this.listSuperheroes(1)

                        this.clearFieldsChangeSuperhero()
                    })
                }
            }
            catch (error) {
                this.isLoading = false

                console.log(error)

                this.toastMensagemErro = "Ocorreu um erro ao tentar excluir o Super-herói."

                this.fecharModal("Excluir")

                $("#toastErro").toast("show")
            }
        },

        // #endregion

        fecharModal(operacao) {
            if (operacao == 'Alterar') {
                $("#modalUpdateSuperhero").removeClass("show");
                $(".modal-backdrop").remove();
                $("#modalUpdateSuperhero").hide();
            } else {
                $("#modalDeleteSuperhero").removeClass("show");
                $(".modal-backdrop").remove();
                $("#modalDeleteSuperhero").hide();
            }
        },
    }
})
    .use(VueLoading.LoadingPlugin)
    .component('loading', VueLoading.Component)
    .component('tablesuperheroesfilters', tableSuperheroesFiltersComponent)
    .component('tablesuperheroes', tableSuperheroesComponent)
    .component('tablesuperheroespagination', tableSuperheroesPaginationComponent)
    .mount('#dvSuperheroes')