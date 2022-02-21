let sum = 0;

function First(price, id, max)
{
    if ((document.getElementById('quantityButton+' + id).value > 0) && (document.getElementById('quantityButton+' + id).value <= parseInt(max)))
    {
        TotalSumChange(price, document.getElementById('quantityButton+' + id).value - parseInt(document.getElementById('valueContainer+' + id).innerHTML));
    }

    document.getElementById('valueContainer+' + id).innerHTML = parseFloat(document.getElementById('quantityButton+' + id).value);
};

function TotalSumChange(price, quantity)
{
    sum = parseFloat(document.getElementById("totalSum").innerHTML);
    sum += parseFloat(price * quantity);
    document.getElementById("totalSum").innerHTML = sum.toString();
};