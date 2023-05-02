# CsvApi

This is the back-end of an interview coding excercise. The excercise asks to create a full stack application which will allow the users to select a local csv file from his machine via a web gui (React JS), send it to a back-end (ASP.NET Web API) which will parse the csv file content and save it in an SQL db. The front-end will finally display the parsed data in a tabular format.

## How to Compile

### React Front-End

1. Open the project with VScode and install dependencies with <b>npm install</b>.
2. Run the project with <b>npm start</b>.
3. The front-end assumes the back-end is served locally on https://localhost:7038.

### ASP.NET Core Web Api

1. Open the solution file with Visual Studio
2. Create a local db called <b>CsvUsers</b> and run the script <b>DbScript.sql</b> using <b>SQL server</b>.
3. Build and Run the project.
4. The back-end allows CORS policies for https://localhost:3000, so front-end needs to be served on that.

## Design Decisions, Limitations and Discussion

### Front-End

- No time to implement viewing multiple csv uploads (back-end supports it though)
- Table has a bug that does not display all values even though they are fetched correctly from the back-end
- Reseting a selected file with the cancel button is bugged.
- NO time to implement unit tests

### Back-End

- Assumes csv has the same structure
- Assumes no multiple newlines per row
- No time to implement unit tests
- Could have normalized data into UserDetails and Addresses tables (alternative approach)
- Could have assumed User to be unique(need a way to identify) and updated user details with subsequent csv file uploads, instead of just saving as new entry. This of course depends on requirement in a real project (discuss further during interview)
