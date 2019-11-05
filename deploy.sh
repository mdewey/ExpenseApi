docker build -t expense-api-image .

docker tag expense-api-image registry.heroku.com/sdg-expense-api/web

docker push registry.heroku.com/sdg-expense-api/web

heroku container:release web -a sdg-expense-api