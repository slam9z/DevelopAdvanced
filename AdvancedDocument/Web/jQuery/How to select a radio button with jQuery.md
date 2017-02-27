[How to select a radio button with jQuery](http://www.mkyong.com/jquery/how-to-select-a-radio-button-with-jquery/)

A simple example to select a radio button with jQuery dynamically.

A radio buttons group, with a name=”sex”.

```html
<input type="radio" name="sex" value="Male">Male</input>
<input type="radio" name="sex" value="Female">Female</input>
<input type="radio" name="sex" value="Unknown">Unknown</input>
```

1. To display the selected radio button value.

$('input:radio[name=sex]:checked').val();

2. To select a radio button (Male).
The radio button is 0-based, so the ‘Male’ = ‘0’, ‘Female’ = ‘1’ and ‘Unknown’ = ‘2’.

$('input:radio[name=sex]:nth(0)').attr('checked',true);
or
$('input:radio[name=sex]')[0].checked = true;

3. To select a radio button (Female).

$('input:radio[name=sex]:nth(1)').attr('checked',true);
or
$('input:radio[name=sex]')[1].checked = true;

4. To select a radio button (Unknown).

$('input:radio[name=sex]:nth(2)').attr('checked',true);
or
$('input:radio[name=sex]')[2].checked = true;

5. To reset the selected radio button.

```js
$('input:radio[name=sex]').attr('checked',false);
```

jQuery select a radio button example

<html>
<head>
<title>jQuery select a radio button example</title>

<script type="text/javascript" src="jquery-1.3.2.min.js"></script>

</head>

<body>

<h1>jQuery select a radio button example</h1>

<script type="text/javascript">

  $(document).ready(function(){

    $("#isSelect").click(function () {

	alert($('input:radio[name=sex]:checked').val());

    });

    $("#selectMale").click(function () {

	$('input:radio[name=sex]:nth(0)').attr('checked',true);
	//$('input:radio[name=sex]')[0].checked = true;

    });

    $("#selectFemale").click(function () {

	$('input:radio[name=sex]:nth(1)').attr('checked',true);
	//$('input:radio[name=sex]')[1].checked = true;

    });

    $("#selectUnknown").click(function () {

	$('input:radio[name=sex]:nth(2)').attr('checked',true);
	//$('input:radio[name=sex]')[2].checked = true;

    });

    $("#reset").click(function () {

	$('input:radio[name=sex]').attr('checked',false);

    });

  });
</script>
</head><body>

<input type="radio" name="sex" value="Male">Male</input>
<input type="radio" name="sex" value="Female">Female</input>
<input type="radio" name="sex" value="Unknown">Unknown</input>

<br/>
<br/>
<br/>

<input type='button' value='Display Selected' id='isSelect'>
<input type='button' value='Select Male' id='selectMale'>
<input type='button' value='Select Female' id='selectFemale'>
<input type='button' value='Select Unknown' id='selectUnknown'>
<input type='button' value='Reset' id='reset'>

</body>
</html>