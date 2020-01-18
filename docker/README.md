# Docker images

## Trassa.B2B.Frontend.Tests

Image is used in CI on test stage. Frontend app tests run on Chrome in headless mode, so image with Chrome installed is required.

Updating image in project registry (don't forget to bump tag version):

```
$ docker build -t registry.gitlab.com/trassa/b2b-portal/frontend/tests:1.0.0 .
docker push registry.gitlab.com/trassa/b2b-portal/frontend/tests:1.0.0
```