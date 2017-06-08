function AliveTable(tableId) {
	var
		table = document.getElementById(tableId),
		rows = table.rows,
		len = rows.length;
	for (var i = 1; i < len; i++) {
		rows[i].onclick = function (e) {
			for (var j = 1; j < len; j++)
				MarkRow(rows[j], "#ffffff");
			MarkRow(e.target.parentNode, "#bbddff");
		}
	}
	function MarkRow(row, clr) {
		for (var i = 0; row && row.cells && i < row.cells.length; i++)
			row.cells[i].style.backgroundColor = clr;
	}
}
