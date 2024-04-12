fetch("coinLore_new.json")
.then(function(response){
    return response.json();
})
.then(function(products){
    let placeholder = document.querySelector("#data-output");
    let out = "";
    for(let product of products){
        out += `
        <tr>
            <td>${product.id}</td>
            <td>${product.name}</td>
            <td>${product.name_id}</td>
            <td>${product.volume_usd}</td>
            <td>${product.active_pairs}</td>
            <td>${product.url}</td>
            <td>${product.country}</td>
        </tr>
    `;
    }

    placeholder.innerHTML = out;
});