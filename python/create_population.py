import torch
from architecture import CarNet

for i in range(20):
    model = CarNet()
    x = torch.rand(1, 5)
    out = model(x)
    torch.onnx.export(model, x, f"C:\\Users\\dadbc\\testCar\\Assets\\Resources\\individual_{i}.onnx")
    torch.save(model, f"neural_models/individual_{i}.pt")

