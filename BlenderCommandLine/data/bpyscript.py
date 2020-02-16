import bpy
bpy.ops.import_scene.fbx(filepath="Q:\\Unity\\BlenderCommandLine\\Assets\\BlenderCommandLine\\test 2.fbx")
bpy.context.scene.objects.active = bpy.context.selected_objects[0]
bpy.ops.object.modifier_add(type='DECIMATE')
bpy.context.object.modifiers["Decimate"].ratio = 0.01
bpy.ops.object.modifier_apply(apply_as='DATA', modifier="Decimate")
bpy.ops.export_scene.fbx(filepath="Q:\\Unity\\BlenderCommandLine\\Assets\\BlenderCommandLine\\test 2.fbx")
quit()