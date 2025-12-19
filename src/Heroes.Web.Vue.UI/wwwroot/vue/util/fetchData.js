/**
 *
 * Modulo: fetchData
 * trata lógica de fetch (Ajax) do sistema e trata fluxos de tela
 *
 **/

const fetchData = {
    async fetchDelete(url) {
        return this._fetchDelete(url);
    },

    async fetchPut(url) {
        return this._fetchPut(url);
    },

    async fetchDeleteJson(url) {
        return this._fetchDeleteJson(url, 'json');
    },

    async fetchPutJson(url) {
        return this._fetchPutJson(url, 'json');
    },

    async fetchDataPutJson(url, dados) {
        return this._fetchDataPut(url, dados, 'json');
    },

    async fetchPostJsonValidation(url, dados) {
        return this._fetchDataPostValidation(url, dados, 'json');
    },

    async fetchPostJson(url, dados) {
        return this._fetchDataPost(url, dados, 'json');
    },

    async fetchPostText(url, dados) {
        return this._fetchDataPost(url, dados, 'text');
    },

    async fetchGetJson(url, options) {
        return this._fetchData(url, options, 'json');
    },

    async fetchGetText(url, options) {
        return this._fetchData(url, options, 'text');
    },

    async _fetchDeleteJson(url, type) {
        let options = {
            method: 'DELETE',
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }
        return this._fetchData(url, options, type);
    },

    async _fetchPutJson(url, type) {
        let options = {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }
        return this._fetchData(url, options, type);
    },

    async _fetchDataPut(url, dados, type) {
        let options = {
            method: 'PUT',
            body: JSON.stringify(dados),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }

        return this._fetchDataPutValidation(url, options, type);
    },

    async _fetchDelete(url, dados) {
        let options = {
            method: 'DELETE',
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }
        let response = await fetch(url, options);
        return response;
    },

    async _fetchPut(url, dados) {
        let options = {
            method: 'PUT',
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }
        let response = await fetch(url, options);
        return response;
    },

    async _fetchDataPostValidation(url, dados, type) {
        let options = {
            method: 'POST',
            body: JSON.stringify(dados),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }

        return this._fetchDataValidation(url, options, type);
    },

    async _fetchDataPost(url, dados, type) {
        //@ts-ignore
        //let requestToken = document.getElementById('RequestVerificationToken').value;

        let options = {
            method: 'POST',
            body: JSON.stringify(dados),
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            }
        }

        return this._fetchData(url, options, type);
    },

    async _fetchDataValidation(url, options, type) {
        let dados = undefined;

        try {
            let response = await fetch(url, options);
            dados = await this._handleResponseValidation(response, type);
        } catch (error) {
            throw error;
        }

        return dados;
    },

    async _fetchDataPutValidation(url, options, type) {
        let dados = undefined;

        try {
            let response = await fetch(url, options);
            dados = await this._handleResponsePutValidation(response, type);
        } catch (error) {
            throw error;
        }

        return dados;
    },

    async _fetchData(url, options, type) {
        let dados = undefined;

        try {
            let response = await fetch(url, options);
            dados = await this._handleResponse(response, type);
        } catch (error) {
            throw error;
        }

        return dados;
    },

    async _handleResponse(response, type) {
        //se tipo não for informado, tenta inferir pelo content-type
        if (type == undefined) {
            type = response.headers.get('content-type');
        }

        return this._parseResponse(response, type);
    },

    async _handleResponseValidation(response, type) {
        if (type == undefined) {
            type = response.headers.get('content-type');
        }
        console.log('handleResponseValidation')
        return this._parseResponseValidation(response, type);
    },

    async _handleResponsePutValidation(response, type) {
        if (type == undefined) {
            type = response.headers.get('content-type');
        }
        console.log('handleResponseValidation')
        return this._parseResponsePutValidation(response, type);
    },

    async _parseResponseValidation(response, type) {
        console.log('parseResponseValidation')
        if (response.ok) {
            console.log('parseResponseValidation OK')
            let dado = undefined;
            if (type.includes('json')) {
                console.log('parseResponseValidation json')
                console.log(response)
                dado = await response.json();
            }
            else if (type.includes('text')) {
                dado = await response.text();
            }
            else {
                console.log('parseResponseValidation Error(`content-type')
                throw new Error(`content-type ${type} não suportado`);
            }
            return dado;
        }
        else {
            if (response.status == 400) {
                let responseJson = await response.json();
                if (responseJson.errors) {
                    console.log('response validation parse json')
                    return responseJson;
                } else {
                    console.log('Error');
                    throw new Error("");
                }
            } else {
                throw new Error("");
            }
        }
    },

    async _parseResponsePutValidation(response, type) {
        if (response.ok) {
            let dado = undefined;
            if (type.includes('json')) {
                dado = response;
            }
            else {
                throw new Error(`content-type ${type} não suportado`);
            }
            return dado;
        }
        else {
            if (response.status == 400) {
                let responseJson = await response.json();
                if (responseJson.errors) {
                    return responseJson;
                } else {
                    throw new Error("");
                }
            } else {
                throw new Error("");
            }
        }
    },

    async _parseResponse(response, type) {
        if (response.ok) {
            let dado = undefined;

            if (type.includes('json')) {
                dado = await response.json();
            }
            else if (type.includes('text')) {
                dado = await response.text();
            }
            else {
                throw new Error(`content-type ${type} não suportado`);
            }
            return dado;
        }
        else {
            throw new Error(`${response.status} - ${response.statusText}`);
        }
    }
}