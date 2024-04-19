url = "http://127.0.0.1:80";
const table_header = document.querySelector("#data-attrs")
const table_content = document.querySelector("#data-contents");

function onSearch() {
    table_content.innerHTML = '';
    table_header.innerHTML = '';
    var search_string = document.querySelector('#search_input').value;
    fetch(url + `/search/${search_string}`)
        .then(function(response) {
            return response.json();
        })
        .then(function(products) {
            if (!products.length) {
                alert('无检索结果！');
                return;
            }

            const tr_h = document.createElement('tr');
            for (const key in products[0]) {
                tr_h.innerHTML += `<th>${key}</th>`;
            }

            table_header.appendChild(tr_h);

            for (const product of products) {
                const tr_c = document.createElement('tr');
                for (const key in product) {
                    const value = product[key];
                    tr_c.innerHTML += `<td>${value || ""}</td>`
                }
                table_content.appendChild(tr_c);
            }

        });
}