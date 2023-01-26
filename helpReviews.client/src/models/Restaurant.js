export class Restaurant {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.exposure = data.exposure
    this.reportCount = data.reportCount
    this.description = data.description
    this.coverImg = data.coverImg
    this.shutdown = data.shutdown
    this.ownerId = data.ownerId
  }
}
