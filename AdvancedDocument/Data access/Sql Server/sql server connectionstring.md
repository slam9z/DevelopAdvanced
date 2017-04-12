## LocalDb stirng

``` xml
 <connectionStrings>
    <add name="DbContext" 
        connectionString="
            Data Source=(LocalDb)\v11.0;
            AttachDbFilename=|DataDirectory|\WebShop.mdf;
            Initial Catalog=WebShop;
            Integrated Security=True" 
    providerName="System.Data.SqlClient" />-->
  </connectionStrings>
  <appSettings></appSettings>
```

## Sql server express

``` xml
 <connectionStrings>
	<add name="DbContext" 
        connectionString="
            Data Source=WIN-QRQVRS3ELM0\SQLEXPRESS;
            Initial Catalog=WebShop;
            Integrated Security=True" 
    providerName="System.Data.SqlClient" />
  </connectionStrings>
  <appSettings></appSettings>
```