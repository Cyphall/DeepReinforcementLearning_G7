from bokeh.plotting import figure, show, output_file
import json
import os

def show_iterations(iterations):
	p = figure(title="Iterations evolution over time", x_axis_label="Evaluations", y_axis_label="Iterations")
	
	iterations_num = []
	for i in range(len(iterations)):
		iterations_num.append(i+1)
	
	p.line(iterations_num, iterations, line_width=2)
	
	output_file("plot1.html")
	show(p)

def show_actions(actions):
	
	action_names = []
	action_counts = []
	for action in actions:
		action_names.append(action["name"])
		action_counts.append(action["count"])
	
	nums = [i+0.5 for i in range(len(action_names))]
	
	p = figure(title="Action counts", y_axis_label="Use count", x_range=action_names)
	p.vbar(x=nums, top=action_counts, width=0.5, bottom=0, color="red")
	
	output_file("plot2.html")
	show(p)

desktop = os.path.join(os.path.join(os.environ['USERPROFILE']), 'Desktop')

with open(desktop+"/RL_stats_result.json") as f:
	json_data = json.load(f)

show_iterations(json_data["iterations"])
show_actions(json_data["actions"])
