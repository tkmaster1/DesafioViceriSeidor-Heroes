// Table
const tableSuperheroesComponent = {
    emits: ['exibirModalEditar', 'exibirModalExcluir'],
    template: `<div class="table-responsive">
                    <table class="table table-striped projects">
                        <thead>
                            <tr>
                                <th style="width: 1%">
                                    #
                                </th>
                                <th style="width: 20%">
                                    Nome
                                </th>
                                <th style="width: 20%">
                                    Nome do herói (único)
                                </th>
                                 <th style="width: 20%">
                                    Data de Nascimento
                                </th>
                                 <th style="width: 10%">
                                    Altura
                                </th>
                                 <th style="width: 10%">
                                    Peso
                                </th>                               
                                <th style="width: 20%" class="text-center">
                                    Ações
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-if="superheroesResponse != undefined && superheroesResponse.length > 0" v-for="sup in superheroesResponse"
                            :key="sup.code">
                                <td>
                                    #
                                </td>
                                <td>
                                    {{sup.name}}
                                </td>
                                <td>
                                    {{sup.heroName}}
                                </td>
                                 <td>
                                    {{moment(sup.birthDate)}}
                                </td>
                                 <td>
                                    {{sup.height}}
                                </td>
                                 <td>
                                    {{sup.weight}}
                                </td>                               
                                <td class="project-actions text-center" style="width: 20%">
                                    <a href="#modalUpdateSuperhero" data-toggle="modal"
                                    class=" w-66px me-1 pt-1" v-on:click="$emit('exibirModalEditar', sup.code)">
                                        <i class="fas fa-edit fa-fw me-1 " title="Editar Herói"></i>
                                    </a>
                                    <a href="#modalDeleteSuperhero" data-bs-toggle="modal"
                                    class=" w-66px me-1 pt-1" v-on:click="$emit('exibirModalExcluir', sup.code)">
                                        <i class="fas fa-trash fa-fw me-1 " style="color:red" title="Remover Herói"></i>
                                    </a>
                                </td>
                            </tr>
                            <tr v-else>
                                <td class="text-center" colspan="8">Nenhum registro encontrado</td>
                            </tr>
                        </tbody>
                    </table>
                 </div>`,
    props: ['superheroesResponse'],
    methods: {        
        moment: function (date) {
            moment.locale('pt-br')
            return moment(date).format('DD/MM/YYYY');
        },
    }
}

// - Pagination

const tableSuperheroesPaginationComponent = {
    emits: ['listSuperheroes'],
    template: `<div class="card-footer clearfix">
                    <div class="d-md-flex align-items-center">
                        <div class="me-md-auto text-md-left mb-2 mb-md-0">
                            Mostrando {{superheroesResponsePagination.currentPage}} de
                            {{superheroesResponsePagination.totalPages}} página(s)
                        </div>
                    </div>
                    <ul class="pagination pagination-sm m-0 float-right">
                        <li class="page-item " v-bind:class="superheroesResponsePagination.currentPage == 1 ? 'disabled' : ''">
                            <a class="page-link" href="#" v-on:click.prevent="$emit('listSuperheroes', 1)">Primeira</a>
                        </li>

                        <li class="page-item " v-bind:class="superheroesResponsePagination.currentPage == 1 ? 'disabled' : ''">
                            <a class="page-link" href="#"
                           v-on:click.prevent="$emit('listSuperheroes', superheroesResponsePagination.currentPage - 1)">«</a>
                        </li>

                        <template v-if="superheroesResponsePagination.count > 0" v-for="pagina in pages">
                            <li class="page-item"
                                v-bind:class="pagina == superheroesResponsePagination.currentPage ? 'active' : ''">
                                <a class="page-link" href="#" v-on:click.prevent="$emit('listSuperheroes', pagina)">{{pagina}}</a>
                            </li>
                        </template>

                        <li class="page-item">
                            <a class="page-link"
                               v-bind:class="(superheroesResponsePagination.currentPage == superheroesResponsePagination.totalPages) || (superheroesResponsePagination.count  == 0) ? 'disabled' : ''"
                               href="#"
                               v-on:click.prevent="$emit('listSuperheroes', superheroesResponsePagination.currentPage + 1)" >»</a>
                        </li>

                        <li class="page-item">
                            <a class="page-link"
                            v-bind:class="(superheroesResponsePagination.currentPage == superheroesResponsePagination.totalPages) || (superheroesResponsePagination.count  == 0) ? 'disabled' : ''"
                            href="#"
                            v-on:click.prevent="$emit('listSuperheroes', superheroesResponsePagination.totalPages)">Última</a>
                        </li>
                    </ul>
                </div>`,
    props: ['superheroesResponsePagination'],
    data() {
        return {
            maxPages: 10,
        }
    },
    computed: {
        pages() {
            const numShown = this.superheroesResponsePagination.totalPages > this.maxPages ?
                this.maxPages : this.superheroesResponsePagination.totalPages;

            let first = this.superheroesResponsePagination.currentPage - Math.floor(numShown / 2);
            first = Math.max(first, 1);
            first = Math.min(first, this.superheroesResponsePagination.totalPages - numShown + 1);

            return [...Array(numShown)].map((k, i) => i + first);
        },
    }
}

// - Filters

const tableSuperheroesFiltersComponent = {
    emits: ['filterFields', 'listSuperheroes', 'clearFilterSuperheroes'],
    template: `
                
                 <div>
                    <div class="row mb-3">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <input type="text" id="filterName" class="form-control" placeholder="Nome"
                                           v-model="filterName" v-on:keyup.enter="$emit('filterFields', 1, filterNameSuperheroes(1))">
                                    <button class="btn btn-default" v-on:click.prevent="$emit('filterFields', 1, filterNameSuperheroes(1))">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="input-group">
                                    <input type="text" id="filterHeroName" class="form-control" placeholder="Nome do herói"
                                           v-model="filterHeroName" v-on:keyup.enter="$emit('filterFields', 2, filterNameSuperheroes(2))">
                                    <button class="btn btn-default" v-on:click.prevent="$emit('filterFields', 2, filterNameSuperheroes(2))">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <input type="text" id="filterCode" class="form-control" placeholder="Código"
                                                   v-model="filterCode" v-on:keyup.enter="$emit('filterFields', 3, filterNameSuperheroes(3))">
                                            <button class="btn btn-default" v-on:click.prevent="$emit('filterFields', 3, filterNameSuperheroes(3))">
                                                <i class="fa fa-search"></i>
                                            </button>
                                        </div>
                                    </div>

                                    <button class="btn btn-secondary" v-on:click="clearFilterSuperheroes();$emit('clearFilterSuperheroes', false)"
                                            type="button">
                                        <span class="d-none d-md-inline">Limpar Filtros</span><span class="d-inline d-md-none"><i class="fa fa-check"></i></span>
                                    </button>

                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>
              </div> `,
    data() {
        return {
            // -- Inicio Filtros --

            filterCode: '',
            filterName: '',
            filterHeroName: '',

            statusFilter: undefined,
            filterNameClicked: false,
            filterHeroNameClicked: false,
            filterCodeClicked: false,

            // -- Fim Filtros --
        }
    },
    methods: {

        async clearFilterSuperheroes() {

            this.filterCode = ''
            this.filterName = ''
            this.filterHeroName = ''
            this.filterNameClicked = false
            this.filterHeroNameClicked = false
            this.filterCodeClicked = false

            this.statusSelected = 0
            this.statusFilter = undefined
        },

        filterNameSuperheroes(filter) {
            //   console.log(filter)
            if (filter == 1)
                return this.filterName
            else if (filter == 2)
                return this.filterHeroName
            else
                return this.filterCode
        }
    },
}