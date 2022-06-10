
import bpy
from bpy import context
import math
from mathutils import Euler
import random

# modified from https://gist.github.com/openroomxyz/f790b1037cd2ffdf6b936e906d232a78
def Union(obj_a, obj_b, new_one_name):
    # Create a boolean modifier named 'my_bool_mod' for the cube.
    mod_bool =  obj_a.modifiers.new('my_bool_mod', 'BOOLEAN')
    # Set the mode of the modifier to DIFFERENCE.
    mod_bool.operation = 'UNION' #'DIFFERENCE'
    # Set the object to be used by the modifier.
    mod_bool.object = obj_b
    bpy.context.view_layer.objects.active = obj_a
    # Apply the modifier.
    res = bpy.ops.object.modifier_apply(modifier = 'my_bool_mod')
    
    obj_b.select_set(True) # 2.8+
    bpy.ops.object.delete()
    obj_a.name = new_one_name
    return res

src_objs = bpy.context.selected_objects
main_obj = None

bpy.ops.object.empty_add(type="SPHERE")
empty = bpy.context.object
empty.name = "surface"

for i in range (0,20):
    src_obj = random.choice(src_objs)
    new_obj = src_obj.copy()
    new_obj.data = src_obj.data.copy()
    context.collection.objects.link(new_obj)
    new_obj.rotation_euler = Euler((random.randrange(0,360), random.randrange(0,360), random.randrange(0,360)), 'XYZ')
    new_obj.name = "TEST"+str(i)
    print("made TEST number " + str(i))
    new_obj.parent = empty
#    if main_obj == None:
#        main_obj = new_obj
#    else:
#        Union(main_obj, new_obj, "unioned surface")