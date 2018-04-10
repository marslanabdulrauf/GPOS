var items = [];

function Data() {
    return {
        text: $('#products').val().trim()
    };
}

function empty() {
    $('#products').data('kendoAutoComplete').value("");
    $('#products').data('kendoAutoComplete').focus();
}

function takeQuantity() {
    do {
        var selection = parseInt(prompt("Please Enter Quantity To Proceed.", "1"));
    } while (isNaN(selection) || selection < 1);
    return selection;
}

function AddTotal(amnt) {
    var prev = parseInt($('#tot').data("kendoNumericTextBox").value());
    var now = prev + amnt;
    var box = $("#tot").data("kendoNumericTextBox");
    box.value(now);
}

function removeItem(data) {
    var i = items.indexOf(data);
    if (i >= 0) {
        items.splice(i, 1);
    }
}

function calBalance() {
    try {
        var rcv = parseInt($('#rcv').data("kendoNumericTextBox").value());
    } catch (e) {
        rcv = 0;
    }
    var bill = parseInt($('#tot').data("kendoNumericTextBox").value());
    var rem = rcv - bill;
    if (rem < 0) {
        alert("Pay Full Amount!");
        document.getElementById('checkout').disabled = true;
        $('#blnce').data("kendoNumericTextBox").value(0);
        $('#products').data('kendoAutoComplete').focus();
        return;
    };
    $('#blnce').data("kendoNumericTextBox").value(rem);
    document.getElementById('checkout').disabled = false;
}
function reCalBalance() {
    var dis;
    try {
        dis = $('#dis').data("kendoNumericTextBox").value();
    } catch (e) {
        $('#dis').data("kendoNumericTextBox").value(0);
        dis = 0;
    } 
    var prev = $('#tot').data("kendoNumericTextBox").value();
    var now = prev - dis;
    $('#tot').data("kendoNumericTextBox").value(now);
}

function FindAndAdd(dataitem,qty) {
    var table = document.getElementById('i_table');
    if (table.rows.length > 1) {
        var i;
        var prev = null;
        for (i=1; i < table.rows.length; i++) {
            if (dataitem.id == table.rows[i].cells[0].innerHTML) {
                prev = table.rows[i];
            }
        }
        if (prev != null) {
            var datastr = prev.cells[0].innerHTML +
                "|" +
                prev.cells[1].innerHTML.trim() +
                "|" +
                prev.cells[2].innerHTML +
                "|" +
                prev.cells[3].innerHTML +
                "|" +
                prev.cells[4].innerHTML;
            i = items.indexOf(datastr);
            items.splice(i, 1);
            var newqty = parseInt(prev.cells[3].innerHTML) + qty;
            var newtotal = newqty * dataitem.retail_price;
            prev.cells[3].innerHTML = newqty;
            prev.cells[4].innerHTML = newtotal;
            datastr = prev.cells[0].innerHTML +
                "|" +
                prev.cells[1].innerHTML.trim() +
                "|" +
                prev.cells[2].innerHTML +
                "|" +
                prev.cells[3].innerHTML +
                "|" +
                prev.cells[4].innerHTML;
            items.push(datastr);
            var box = $("#tot").data("kendoNumericTextBox");
            box.value(newtotal);
            var btn = document.getElementById(dataitem.id);
            btn.onclick = function() {

                AddTotal(-1 * newtotal);
                removeItem(datastr);
                var dv = this.parentElement.parentElement;
                dv.remove();
                document.getElementById('blnce').value = 0;
                document.getElementById('rcv').value = 0;
                document.getElementById('checkout').disabled = true;
            }
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}

function getOrder(e) {
    var dataitem = this.dataItem(e.item.index());
    var qty = takeQuantity();
    if (dataitem.qty < qty) {
        document.getElementById('errmsg').innerHTML = "Remaining "+ dataitem.name +" : " + dataitem.qty;
        $('#alertModel').modal();
        $('#products').data('kendoAutoComplete').focus();
        return;
    }
    var tr = document.createElement("tr");
    var td0 = document.createElement("td");
    var td1 = document.createElement("td");
    var td2 = document.createElement("td");
    var td3 = document.createElement("td");
    var td4 = document.createElement("td");
    var td5 = document.createElement("td");

    var total = dataitem.retail_price * qty;
    var datastr = dataitem.id + "|" + dataitem.name.trim() + "|" + dataitem.retail_price + "|" + qty + "|" + total;

    if (!FindAndAdd(dataitem, qty)) {
        var t0 = document.createTextNode(" " + dataitem.id);
        var t1 = document.createTextNode(" " + dataitem.name);
        var t2 = document.createTextNode(" " + dataitem.retail_price);
        var t3 = document.createTextNode(" " + qty);
        var t4 = document.createTextNode(" " + total);
        var t5 = document.createTextNode(" " + "\u00D7");
        var btn = document.createElement("button");
        btn.setAttribute("class", "btn btn-block btn-warning");
        btn.appendChild(t5);
        btn.tabIndex = -1;
        btn.id = dataitem.id;
        btn.onclick = function() {
            AddTotal(-1 * total);
            removeItem(datastr);
            var dv = this.parentElement.parentElement;
            dv.remove();
            document.getElementById('blnce').value = 0;
            document.getElementById('rcv').value = 0;
            document.getElementById('checkout').disabled = true;
        };
        td0.appendChild(t0);
        td1.appendChild(t1);
        td2.appendChild(t2);
        td3.appendChild(t3);
        td4.appendChild(t4);
        td5.appendChild(btn);
        tr.appendChild(td0);
        tr.appendChild(td1);
        tr.appendChild(td2);
        tr.appendChild(td3);
        tr.appendChild(td4);
        tr.appendChild(td5);
        document.getElementById('i_table').appendChild(tr);

        AddTotal(total);
        items.push(datastr);
    }
    document.getElementById('blnce').value = 0;
    document.getElementById('rcv').value = 0;
    document.getElementById('checkout').disabled = true;
}

$(document).ready(function () {
    $('#tot').kendoNumericTextBox().data("kendoNumericTextBox").enable(false);
    $('#dis').kendoNumericTextBox();
    $('#rcv').kendoNumericTextBox();
    $('#blnce').kendoNumericTextBox().data("kendoNumericTextBox").enable(false);
    $('#products').data('kendoAutoComplete').focus();
});


function checkout() {
    if (items.length === 0) {
        return;
    } else {
        document.getElementById('checkout').disable = true;
        var tot = $('#tot').val();
        var dis = $('#dis').val();
        var rcv = $('#rcv').val(); 
        $.ajax({
            type: "POST",
            url: "http://localhost:62546/api/Order/SetOrder?items=" +
                items +
                "&tot=" +
                tot +
                "&dis=" +
                dis +
                "&rcv=" +
                rcv,
            success: function(res) {
                location.reload();
            },
            error: function(res) {
                document.getElementById('errmsg').innerHTML = res.IncomingMessage;
                $('#alertModel').modal();
                $('#products').data('kendoAutoComplete').focus();
                document.getElementById('checkout').disabled = false;
                return;
            }
    });
    }
}


//customer functions
function customer_data() { 
    return {
        text: $('#customer').val().trim()
    };
}

function setCustomer() {
    
}