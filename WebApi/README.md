## WebApi

To start using WebApi, set WebApi project as StartUp Project in the solution.
Then run the project.
Note the port number may differ.

### Job Offer operations

To get all available job offers:

```
curl -i -X GET http://localhost:7425/api/joboffers
```

To get job offers filtered by a skill:

```
curl -i -X GET http://localhost:7425/api/joboffers?skill=unix
```

To get specific job offer by its ID:

```
curl -i -X GET http://localhost:7425/api/joboffers/2
```

