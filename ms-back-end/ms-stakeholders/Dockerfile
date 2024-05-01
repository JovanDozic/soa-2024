FROM golang:alpine AS ms-stakeholders-builder
WORKDIR /app
COPY go.mod .
COPY go.sum .
RUN go mod download
COPY . .
RUN go build -o ms-stakeholders-webapp

FROM alpine
COPY --from=ms-stakeholders-builder /app/ms-stakeholders-webapp /usr/bin/ms-stakeholders-webapp
EXPOSE 8084
ENTRYPOINT ["/usr/bin/ms-stakeholders-webapp"]
