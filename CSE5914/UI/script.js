fetch("processs.json")
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
            <td>${product.rank}</td>
            <td>${product.symbol}</td>
            <td>${product.name}</td>
            <td>${product.supply}</td>
            <td>${product.maxSupply}</td>
            <td>${product.marketCapUsd}</td>
            <td>${product.volumeUsd24Hr}</td>
            <td>${product.priceUsd}</td>
            <td>${product.changePercent24Hr}</td>
            <td>${product.vwap24Hr}</td>
            <td>${product.explorer}</td>
            
        </tr>
    `;
    }

    placeholder.innerHTML = out;
});