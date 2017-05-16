[HTML Table width in percentage, table rows separated equaly](http://stackoverflow.com/questions/7700534/html-table-width-in-percentage-table-rows-separated-equaly)


## answer

This is definitely the cleanest answer to the question: http://stackoverflow.com/a/14025331/1008519. In combination with table-layout: fixed I often find <colgroup> a great tool to make columns act as you want (see codepen here):

```css
table {
 /* When set to 'fixed', all columns that do not have a width applied will get the remaining space divided between them equally */
 table-layout: fixed;
}
.fixed-width {
  width: 100px;
}
.col-12 {
  width: 100%;
}
.col-11 {
  width: 91.666666667%;
}
.col-10 {
  width: 83.333333333%;
}
.col-9 {
  width: 75%;
}
.col-8 {
  width: 66.666666667%;
}
.col-7 {
  width: 58.333333333%;
}
.col-6 {
  width: 50%;
}
.col-5 {
  width: 41.666666667%;
}
.col-4 {
  width: 33.333333333%;
}
.col-3 {
  width: 25%;
}
.col-2 {
  width: 16.666666667%;
}
.col-1 {
  width: 8.3333333333%;
}

/* Stylistic improvements from here */

.align-left {
  text-align: left;
}
.align-right {
  text-align: right;
}
table {
  width: 100%;
}
table > tbody > tr > td,
table > thead > tr > th {
  padding: 8px;
  border: 1px solid gray;
}
```

```html
<table cellpadding="0" cellspacing="0" border="0">
  <colgroup>
    <col /> <!-- take up rest of the space -->
    <col class="fixed-width" /> <!-- fixed width -->
    <col class="col-3" /> <!-- percentage width -->
    <col /> <!-- take up rest of the space -->
  </colgroup>
  <thead>
    <tr>
      <th class="align-left">Title</th>
      <th class="align-right">Count</th>
      <th class="align-left">Name</th>
      <th class="align-left">Single</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td class="align-left">This is a very looooooooooong title that may break into multiple lines</td>
      <td class="align-right">19</td>
      <td class="align-left">Lisa McArthur</td>
      <td class="align-left">No</td>
    </tr>
    <tr>
      <td class="align-left">This is a shorter title</td>
      <td class="align-right">2</td>
      <td class="align-left">John Oliver Nielson McAllister</td>
      <td class="align-left">Yes</td>
    </tr>
  </tbody>
</table>


<table cellpadding="0" cellspacing="0" border="0">
  <!-- define everything with percentage width -->
  <colgroup>
    <col class="col-6" />
    <col class="col-1" />
    <col class="col-4" />
    <col class="col-1" />
  </colgroup>
  <thead>
    <tr>
      <th class="align-left">Title</th>
      <th class="align-right">Count</th>
      <th class="align-left">Name</th>
      <th class="align-left">Single</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td class="align-left">This is a very looooooooooong title that may break into multiple lines</td>
      <td class="align-right">19</td>
      <td class="align-left">Lisa McArthur</td>
      <td class="align-left">No</td>
    </tr>
    <tr>
      <td class="align-left">This is a shorter title</td>
      <td class="align-right">2</td>
      <td class="align-left">John Oliver Nielson McAllister</td>
      <td class="align-left">Yes</td>
    </tr>
  </tbody>
</table>
```