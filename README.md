# Superedit
## VeeamHub are community driven projects, and are not created by Veeam R&D nor validated by Veeam Q&A. They are maintained by community members which might be or not be Veeam employees. 

## Distributed under MIT license
Copyright (c) 2016 VeeamHub

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


**Author:** Timothy Dewin

**Function:** Allowing you to enforce settings on multiple Veeam Objects (e.g. jobs) 

**Requires:** Backup & Replication v9 (As per veeampssnapin, needs to be run on the backup server as Administrator. If you don't run as administrator, you might get an empty view)

**Stability:** Alpha, only use in test environments

**How it works:** It is a basically a small csharp GUI wrapper around predefined powershell script, with the possibility to select indiviual objects. This allows unexperienced powershell users to mass edit settings without knowing any powershell code.

**Usage:** 

Download the compiled version in nightlybuild. GUI should be straight forward. Execute will just execute the generated powershell code without validation.

![Screenshot Superedit Main](https://github.com/veeamhub/Superedit/img/main.png)

Review allows you to review the powershell code generated.

![Screenshot Superedit Review](https://github.com/veeamhub/Superedit/img/review.png)

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
