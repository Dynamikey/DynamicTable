Current folder structure required for app to function (could be changed within code):

Main folder: User Defined Path\\RR

Folders within RR:
	CSV Output: ...\\RR\\CSVFolder
	Repair Note Figures: ...\\RN\\RNFigures\\"Repair Note Number e.g. "RN-EJ-412-1009-03""
	Camera Picture Output: ...\\RR\\CameraPicsFolder
	XMLs should be placed in: ...\\RR\\XMLFolder

Other files required:
	default.png within ...\\RN\\RNFigures
	EJ200 Repair Note Finder.xlsx within ...\\RN

Figures should be labelled "1A", "1B", "2", "3A" etc within the figure folder.




To change path, change the path value within the .config file found within Application Files.
The app can either show a specific XML file that the students had access to, or any XML. Change the "AnyXML?" setting within the .config file to "Yes" to allow any XML, or "No" for a single pre-defined XML.
There may be issues opening other XML files, the Cambridge team had access to two XML files only for which the code funcions correctly. May require changes to the XML parsing code.