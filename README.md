# BradyChallenge
This is for Brady PLC challenge, this program reads an XML file, does calculations on the values that reads and outputs the results. It contains file watcher, if 
the file basic 01-Basic.xml is created or changed it will generate a new 01-Basic-Result.xml

File location is set on App.config, currently I set it to my desktop, this needs to be adapted to a user on App.config.

This program is using .net Core 3.1


Assumptions:
 - It assumes that there is daily generation data for coal and gas for each gas and coal generators.
 - I assumed a location for the files since there is no requirement for that

Goals achieved:
  - Read XML file from input file
  - Read XML file with reference data
  - Implement file watcher for creation and change of XML file
  - Perform calculations based on input and reference data files
  - Create XML output file to folder
  - Used App.config for input file name, output file name, reference data file name and files location
  
Not achieved:
  - Test coverage
 
To improve:
  - It fails the criteria of the challenge on test coverage, I did not add test coverage because of lack of time
  - Cover the scenario where a gas or coal generators are not operational and therefore there might not be data on a specific day for it
  The task description was explicit about this.
  - The location of the files was not explicit and I picked one
  
