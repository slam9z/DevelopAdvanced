[Min-width and max-height for table attributes](http://stackoverflow.com/questions/6426779/min-width-and-max-height-for-table-attributes)


For table cells the 'width' property should be used, as the 'min-width' and 'max-width' is undefined 
for table cells. See the specification:

"In CSS 2.1, the effect of 'min-width' and 'max-width' on tables, inline tables, table cells, table
 columns, and column groups is undefined."

To enforce the width, you may try to change the table-layout property to "fixed". The specification 
describes the algorithm pretty clear.