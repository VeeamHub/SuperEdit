# SuperEdit

**Author:** Timothy Dewin

**Function:** Allowing you to enforce settings on multiple Veeam Objects (e.g. jobs) 

## 📗 Documentation

**Requires:** Backup & Replication v9 (As per veeampssnapin, needs to be run on the backup server as Administrator. If you don't run as administrator, you might get an empty view)

**Stability:** Alpha, only use in test environments

**How it works:** It is a basically a small csharp GUI wrapper around predefined powershell script, with the possibility to select indiviual objects. This allows unexperienced powershell users to mass edit settings without knowing any powershell code.

**Usage:**

Download the compiled version in nightlybuild. GUI should be straight forward. Execute will just execute the generated powershell code without validation.

![Screenshot Superedit Main](https://github.com/VeeamHub/SuperEdit/raw/master/img/main.png)

Review allows you to review the powershell code generated.

![Screenshot Superedit Review](https://github.com/VeeamHub/SuperEdit/raw/master/img/review.png)

If you want to contribute in the form of Powershell code, you can push F11. This will extract the embedded Templates.xml. You can edit and restart the application to activate the custom Templates.xml in the working directory.  

Here is an example of a complete object template including one script template. Notice that for backup jobs an object template is already defined, and you can just add multiple "template" section in objecttemplate

```XML
<!-- Name of the object, shown in the topright corner-->
  <objectTemplate name="Backup Hyperv">
	<!-- Objectfilter is used to fill up the gridview when an objecttemplate is selected in the topright corner. After the object filter a "| select" statement is added, selecting only the field defined in the select attribute-->
	<!-- The objectfilter is also used without the "| select" section to select indiviual objects for editing (code generation) -->
    <objectfilter select="Name">
      <![CDATA[
get-vbrjob | ? { $_.jobtype -eq "Backup" -and $_.BackupPlatform -eq "EHyperV" }
]]>
    </objectfilter>
	<!-- Template script. Name attribute is used to fill up the combobox in the left bottom corner, defining the action-->
    <template name="Swap File Exclusion">
	  <!-- Script that will be executed on every object. 2 variables are automatically defined by the code generation -->
	  <!-- $value : the value selected by the user -->
	  <!-- $obj : the current object that you are working on -->
      <script>
        <![CDATA[
$opt = $obj | get-vbrjoboptions
$opt.HvSourceOptions.ExcludeSwapFile = $value
$obj | set-vbrjoboptions -Options $opt
]]>
      </script>
	  <!-- Values are used to fill up the second combobox in the right corner -->
	  <!-- The innertext between the tags is used to fill up the value tag. However if you want another value to be passed into $value, you can use real to override the "display" value-->
	  <!-- The code will also try to execute ParseInt on the value. If it fails, it will surround the value with double quotes -->
	  <!-- For $false and $true, you can use 0 and 1 as shown in the example below -->
      <values>
        <value real="1">Enabled</value>
        <value real="0">Disabled</value>
      </values>
    </template>
  </objectTemplate>
```

## ✍ Contributions

We welcome contributions from the community! We encourage you to create [issues](https://github.com/VeeamHub/SuperEdit/issues/new/choose) for Bugs & Feature Requests and submit Pull Requests for improving our documentation. For more detailed information, refer to our [Contributing Guide](CONTRIBUTING.md).

## 🤝🏾 License

* [MIT License](LICENSE)

## 🤔 Questions

If you have any questions or something is unclear, please don't hesitate to [create an issue](https://github.com/VeeamHub/SuperEdit/issues/new/choose) and let us know!
