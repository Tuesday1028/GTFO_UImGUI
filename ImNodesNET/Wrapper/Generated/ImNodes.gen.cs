using ImGuiNET;
using System;
using System.Text;
using UnityEngine;

namespace ImNodesNET
{
    public static unsafe partial class ImNodes
    {
        public static void BeginInputAttribute(int id)
        {
            ImNodesPinShape shape = ImNodesPinShape.CircleFilled;
            ImNodesNative.ImNodes_BeginInputAttribute(id, shape);
        }
        public static void BeginInputAttribute(int id, ImNodesPinShape shape)
        {
            ImNodesNative.ImNodes_BeginInputAttribute(id, shape);
        }
        public static void BeginNode(int id)
        {
            ImNodesNative.ImNodes_BeginNode(id);
        }
        public static void BeginNodeEditor()
        {
            ImNodesNative.ImNodes_BeginNodeEditor();
        }
        public static void BeginNodeTitleBar()
        {
            ImNodesNative.ImNodes_BeginNodeTitleBar();
        }
        public static void BeginOutputAttribute(int id)
        {
            ImNodesPinShape shape = ImNodesPinShape.CircleFilled;
            ImNodesNative.ImNodes_BeginOutputAttribute(id, shape);
        }
        public static void BeginOutputAttribute(int id, ImNodesPinShape shape)
        {
            ImNodesNative.ImNodes_BeginOutputAttribute(id, shape);
        }
        public static void BeginStaticAttribute(int id)
        {
            ImNodesNative.ImNodes_BeginStaticAttribute(id);
        }
        public static void ClearLinkSelection()
        {
            ImNodesNative.ImNodes_ClearLinkSelection_Nil();
        }
        public static void ClearLinkSelection(int link_id)
        {
            ImNodesNative.ImNodes_ClearLinkSelection_Int(link_id);
        }
        public static void ClearNodeSelection()
        {
            ImNodesNative.ImNodes_ClearNodeSelection_Nil();
        }
        public static void ClearNodeSelection(int node_id)
        {
            ImNodesNative.ImNodes_ClearNodeSelection_Int(node_id);
        }
        public static ImNodesContextPtr CreateContext()
        {
            ImNodesContext* ret = ImNodesNative.ImNodes_CreateContext();
            return new ImNodesContextPtr(ret);
        }
        public static void DestroyContext()
        {
            ImNodesContext* ctx = null;
            ImNodesNative.ImNodes_DestroyContext(ctx);
        }
        public static void DestroyContext(ImNodesContextPtr ctx)
        {
            ImNodesContext* native_ctx = ctx.NativePtr;
            ImNodesNative.ImNodes_DestroyContext(native_ctx);
        }
        public static ImNodesEditorContextPtr EditorContextCreate()
        {
            ImNodesEditorContext* ret = ImNodesNative.ImNodes_EditorContextCreate();
            return new ImNodesEditorContextPtr(ret);
        }
        public static void EditorContextFree(ImNodesEditorContextPtr noname1)
        {
            ImNodesEditorContext* native_noname1 = noname1.NativePtr;
            ImNodesNative.ImNodes_EditorContextFree(native_noname1);
        }
        public static Vector2 EditorContextGetPanning()
        {
            Vector2 __retval;
            ImNodesNative.ImNodes_EditorContextGetPanning(&__retval);
            return __retval;
        }
        public static void EditorContextMoveToNode(int node_id)
        {
            ImNodesNative.ImNodes_EditorContextMoveToNode(node_id);
        }
        public static void EditorContextResetPanning(Vector2 pos)
        {
            ImNodesNative.ImNodes_EditorContextResetPanning(pos);
        }
        public static void EditorContextSet(ImNodesEditorContextPtr noname1)
        {
            ImNodesEditorContext* native_noname1 = noname1.NativePtr;
            ImNodesNative.ImNodes_EditorContextSet(native_noname1);
        }
        public static void EndInputAttribute()
        {
            ImNodesNative.ImNodes_EndInputAttribute();
        }
        public static void EndNode()
        {
            ImNodesNative.ImNodes_EndNode();
        }
        public static void EndNodeEditor()
        {
            ImNodesNative.ImNodes_EndNodeEditor();
        }
        public static void EndNodeTitleBar()
        {
            ImNodesNative.ImNodes_EndNodeTitleBar();
        }
        public static void EndOutputAttribute()
        {
            ImNodesNative.ImNodes_EndOutputAttribute();
        }
        public static void EndStaticAttribute()
        {
            ImNodesNative.ImNodes_EndStaticAttribute();
        }
        public static ImNodesContextPtr GetCurrentContext()
        {
            ImNodesContext* ret = ImNodesNative.ImNodes_GetCurrentContext();
            return new ImNodesContextPtr(ret);
        }
        public static ImNodesIOPtr GetIO()
        {
            ImNodesIO* ret = ImNodesNative.ImNodes_GetIO();
            return new ImNodesIOPtr(ret);
        }
        public static Vector2 GetNodeDimensions(int id)
        {
            Vector2 __retval;
            ImNodesNative.ImNodes_GetNodeDimensions(&__retval, id);
            return __retval;
        }
        public static Vector2 GetNodeEditorSpacePos(int node_id)
        {
            Vector2 __retval;
            ImNodesNative.ImNodes_GetNodeEditorSpacePos(&__retval, node_id);
            return __retval;
        }
        public static Vector2 GetNodeGridSpacePos(int node_id)
        {
            Vector2 __retval;
            ImNodesNative.ImNodes_GetNodeGridSpacePos(&__retval, node_id);
            return __retval;
        }
        public static Vector2 GetNodeScreenSpacePos(int node_id)
        {
            Vector2 __retval;
            ImNodesNative.ImNodes_GetNodeScreenSpacePos(&__retval, node_id);
            return __retval;
        }
        public static void GetSelectedLinks(ref int link_ids)
        {
            fixed (int* native_link_ids = &link_ids)
            {
                ImNodesNative.ImNodes_GetSelectedLinks(native_link_ids);
            }
        }
        public static void GetSelectedNodes(ref int node_ids)
        {
            fixed (int* native_node_ids = &node_ids)
            {
                ImNodesNative.ImNodes_GetSelectedNodes(native_node_ids);
            }
        }
        public static ImNodesStylePtr GetStyle()
        {
            ImNodesStyle* ret = ImNodesNative.ImNodes_GetStyle();
            return new ImNodesStylePtr(ret);
        }
        public static bool IsAnyAttributeActive()
        {
            int* attribute_id = null;
            byte ret = ImNodesNative.ImNodes_IsAnyAttributeActive(attribute_id);
            return ret != 0;
        }
        public static bool IsAnyAttributeActive(ref int attribute_id)
        {
            fixed (int* native_attribute_id = &attribute_id)
            {
                byte ret = ImNodesNative.ImNodes_IsAnyAttributeActive(native_attribute_id);
                return ret != 0;
            }
        }
        public static bool IsAttributeActive()
        {
            byte ret = ImNodesNative.ImNodes_IsAttributeActive();
            return ret != 0;
        }
        public static bool IsEditorHovered()
        {
            byte ret = ImNodesNative.ImNodes_IsEditorHovered();
            return ret != 0;
        }
        public static bool IsLinkCreated(ref int started_at_attribute_id, ref int ended_at_attribute_id)
        {
            byte* created_from_snap = null;
            fixed (int* native_started_at_attribute_id = &started_at_attribute_id)
            {
                fixed (int* native_ended_at_attribute_id = &ended_at_attribute_id)
                {
                    byte ret = ImNodesNative.ImNodes_IsLinkCreated_BoolPtr(native_started_at_attribute_id, native_ended_at_attribute_id, created_from_snap);
                    return ret != 0;
                }
            }
        }
        public static bool IsLinkCreated(ref int started_at_attribute_id, ref int ended_at_attribute_id, ref bool created_from_snap)
        {
            byte native_created_from_snap_val = created_from_snap ? (byte)1 : (byte)0;
            byte* native_created_from_snap = &native_created_from_snap_val;
            fixed (int* native_started_at_attribute_id = &started_at_attribute_id)
            {
                fixed (int* native_ended_at_attribute_id = &ended_at_attribute_id)
                {
                    byte ret = ImNodesNative.ImNodes_IsLinkCreated_BoolPtr(native_started_at_attribute_id, native_ended_at_attribute_id, native_created_from_snap);
                    created_from_snap = native_created_from_snap_val != 0;
                    return ret != 0;
                }
            }
        }
        public static bool IsLinkCreated(ref int started_at_node_id, ref int started_at_attribute_id, ref int ended_at_node_id, ref int ended_at_attribute_id)
        {
            byte* created_from_snap = null;
            fixed (int* native_started_at_node_id = &started_at_node_id)
            {
                fixed (int* native_started_at_attribute_id = &started_at_attribute_id)
                {
                    fixed (int* native_ended_at_node_id = &ended_at_node_id)
                    {
                        fixed (int* native_ended_at_attribute_id = &ended_at_attribute_id)
                        {
                            byte ret = ImNodesNative.ImNodes_IsLinkCreated_IntPtr(native_started_at_node_id, native_started_at_attribute_id, native_ended_at_node_id, native_ended_at_attribute_id, created_from_snap);
                            return ret != 0;
                        }
                    }
                }
            }
        }
        public static bool IsLinkCreated(ref int started_at_node_id, ref int started_at_attribute_id, ref int ended_at_node_id, ref int ended_at_attribute_id, ref bool created_from_snap)
        {
            byte native_created_from_snap_val = created_from_snap ? (byte)1 : (byte)0;
            byte* native_created_from_snap = &native_created_from_snap_val;
            fixed (int* native_started_at_node_id = &started_at_node_id)
            {
                fixed (int* native_started_at_attribute_id = &started_at_attribute_id)
                {
                    fixed (int* native_ended_at_node_id = &ended_at_node_id)
                    {
                        fixed (int* native_ended_at_attribute_id = &ended_at_attribute_id)
                        {
                            byte ret = ImNodesNative.ImNodes_IsLinkCreated_IntPtr(native_started_at_node_id, native_started_at_attribute_id, native_ended_at_node_id, native_ended_at_attribute_id, native_created_from_snap);
                            created_from_snap = native_created_from_snap_val != 0;
                            return ret != 0;
                        }
                    }
                }
            }
        }
        public static bool IsLinkDestroyed(ref int link_id)
        {
            fixed (int* native_link_id = &link_id)
            {
                byte ret = ImNodesNative.ImNodes_IsLinkDestroyed(native_link_id);
                return ret != 0;
            }
        }
        public static bool IsLinkDropped()
        {
            int* started_at_attribute_id = null;
            byte including_detached_links = 1;
            byte ret = ImNodesNative.ImNodes_IsLinkDropped(started_at_attribute_id, including_detached_links);
            return ret != 0;
        }
        public static bool IsLinkDropped(ref int started_at_attribute_id)
        {
            byte including_detached_links = 1;
            fixed (int* native_started_at_attribute_id = &started_at_attribute_id)
            {
                byte ret = ImNodesNative.ImNodes_IsLinkDropped(native_started_at_attribute_id, including_detached_links);
                return ret != 0;
            }
        }
        public static bool IsLinkDropped(ref int started_at_attribute_id, bool including_detached_links)
        {
            byte native_including_detached_links = including_detached_links ? (byte)1 : (byte)0;
            fixed (int* native_started_at_attribute_id = &started_at_attribute_id)
            {
                byte ret = ImNodesNative.ImNodes_IsLinkDropped(native_started_at_attribute_id, native_including_detached_links);
                return ret != 0;
            }
        }
        public static bool IsLinkHovered(ref int link_id)
        {
            fixed (int* native_link_id = &link_id)
            {
                byte ret = ImNodesNative.ImNodes_IsLinkHovered(native_link_id);
                return ret != 0;
            }
        }
        public static bool IsLinkSelected(int link_id)
        {
            byte ret = ImNodesNative.ImNodes_IsLinkSelected(link_id);
            return ret != 0;
        }
        public static bool IsLinkStarted(ref int started_at_attribute_id)
        {
            fixed (int* native_started_at_attribute_id = &started_at_attribute_id)
            {
                byte ret = ImNodesNative.ImNodes_IsLinkStarted(native_started_at_attribute_id);
                return ret != 0;
            }
        }
        public static bool IsNodeHovered(ref int node_id)
        {
            fixed (int* native_node_id = &node_id)
            {
                byte ret = ImNodesNative.ImNodes_IsNodeHovered(native_node_id);
                return ret != 0;
            }
        }
        public static bool IsNodeSelected(int node_id)
        {
            byte ret = ImNodesNative.ImNodes_IsNodeSelected(node_id);
            return ret != 0;
        }
        public static bool IsPinHovered(ref int attribute_id)
        {
            fixed (int* native_attribute_id = &attribute_id)
            {
                byte ret = ImNodesNative.ImNodes_IsPinHovered(native_attribute_id);
                return ret != 0;
            }
        }
        public static void Link(int id, int start_attribute_id, int end_attribute_id)
        {
            ImNodesNative.ImNodes_Link(id, start_attribute_id, end_attribute_id);
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        public static void LoadCurrentEditorStateFromIniFile(ReadOnlySpan<char> file_name)
        {
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_LoadCurrentEditorStateFromIniFile(native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
#endif
        public static void LoadCurrentEditorStateFromIniFile(string file_name)
        {
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_LoadCurrentEditorStateFromIniFile(native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        public static void LoadCurrentEditorStateFromIniString(ReadOnlySpan<char> data, uint data_size)
        {
            byte* native_data;
            int data_byteCount = 0;
            if (data != null)
            {
                data_byteCount = Encoding.UTF8.GetByteCount(data);
                if (data_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_data = Util.Allocate(data_byteCount + 1);
                }
                else
                {
                    byte* native_data_stackBytes = stackalloc byte[data_byteCount + 1];
                    native_data = native_data_stackBytes;
                }
                int native_data_offset = Util.GetUtf8(data, native_data, data_byteCount);
                native_data[native_data_offset] = 0;
            }
            else { native_data = null; }
            ImNodesNative.ImNodes_LoadCurrentEditorStateFromIniString(native_data, data_size);
            if (data_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_data);
            }
        }
#endif
        public static void LoadCurrentEditorStateFromIniString(string data, uint data_size)
        {
            byte* native_data;
            int data_byteCount = 0;
            if (data != null)
            {
                data_byteCount = Encoding.UTF8.GetByteCount(data);
                if (data_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_data = Util.Allocate(data_byteCount + 1);
                }
                else
                {
                    byte* native_data_stackBytes = stackalloc byte[data_byteCount + 1];
                    native_data = native_data_stackBytes;
                }
                int native_data_offset = Util.GetUtf8(data, native_data, data_byteCount);
                native_data[native_data_offset] = 0;
            }
            else { native_data = null; }
            ImNodesNative.ImNodes_LoadCurrentEditorStateFromIniString(native_data, data_size);
            if (data_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_data);
            }
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        public static void LoadEditorStateFromIniFile(ImNodesEditorContextPtr editor, ReadOnlySpan<char> file_name)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_LoadEditorStateFromIniFile(native_editor, native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
#endif
        public static void LoadEditorStateFromIniFile(ImNodesEditorContextPtr editor, string file_name)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_LoadEditorStateFromIniFile(native_editor, native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        public static void LoadEditorStateFromIniString(ImNodesEditorContextPtr editor, ReadOnlySpan<char> data, uint data_size)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            byte* native_data;
            int data_byteCount = 0;
            if (data != null)
            {
                data_byteCount = Encoding.UTF8.GetByteCount(data);
                if (data_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_data = Util.Allocate(data_byteCount + 1);
                }
                else
                {
                    byte* native_data_stackBytes = stackalloc byte[data_byteCount + 1];
                    native_data = native_data_stackBytes;
                }
                int native_data_offset = Util.GetUtf8(data, native_data, data_byteCount);
                native_data[native_data_offset] = 0;
            }
            else { native_data = null; }
            ImNodesNative.ImNodes_LoadEditorStateFromIniString(native_editor, native_data, data_size);
            if (data_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_data);
            }
        }
#endif
        public static void LoadEditorStateFromIniString(ImNodesEditorContextPtr editor, string data, uint data_size)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            byte* native_data;
            int data_byteCount = 0;
            if (data != null)
            {
                data_byteCount = Encoding.UTF8.GetByteCount(data);
                if (data_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_data = Util.Allocate(data_byteCount + 1);
                }
                else
                {
                    byte* native_data_stackBytes = stackalloc byte[data_byteCount + 1];
                    native_data = native_data_stackBytes;
                }
                int native_data_offset = Util.GetUtf8(data, native_data, data_byteCount);
                native_data[native_data_offset] = 0;
            }
            else { native_data = null; }
            ImNodesNative.ImNodes_LoadEditorStateFromIniString(native_editor, native_data, data_size);
            if (data_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_data);
            }
        }
        public static void MiniMap()
        {
            float minimap_size_fraction = 0.2f;
            ImNodesMiniMapLocation location = ImNodesMiniMapLocation.TopLeft;
            ImNodesMiniMapNodeHoveringCallback node_hovering_callback = null;
            ImNodesMiniMapNodeHoveringCallbackUserData node_hovering_callback_data = null;
            ImNodesNative.ImNodes_MiniMap(minimap_size_fraction, location, node_hovering_callback, node_hovering_callback_data);
        }
        public static void MiniMap(float minimap_size_fraction)
        {
            ImNodesMiniMapLocation location = ImNodesMiniMapLocation.TopLeft;
            ImNodesMiniMapNodeHoveringCallback node_hovering_callback = null;
            ImNodesMiniMapNodeHoveringCallbackUserData node_hovering_callback_data = null;
            ImNodesNative.ImNodes_MiniMap(minimap_size_fraction, location, node_hovering_callback, node_hovering_callback_data);
        }
        public static void MiniMap(float minimap_size_fraction, ImNodesMiniMapLocation location)
        {
            ImNodesMiniMapNodeHoveringCallback node_hovering_callback = null;
            ImNodesMiniMapNodeHoveringCallbackUserData node_hovering_callback_data = null;
            ImNodesNative.ImNodes_MiniMap(minimap_size_fraction, location, node_hovering_callback, node_hovering_callback_data);
        }
        public static void MiniMap(float minimap_size_fraction, ImNodesMiniMapLocation location, ImNodesMiniMapNodeHoveringCallback node_hovering_callback)
        {
            ImNodesMiniMapNodeHoveringCallbackUserData node_hovering_callback_data = null;
            ImNodesNative.ImNodes_MiniMap(minimap_size_fraction, location, node_hovering_callback, node_hovering_callback_data);
        }
        public static void MiniMap(float minimap_size_fraction, ImNodesMiniMapLocation location, ImNodesMiniMapNodeHoveringCallback node_hovering_callback, ImNodesMiniMapNodeHoveringCallbackUserData node_hovering_callback_data)
        {
            ImNodesNative.ImNodes_MiniMap(minimap_size_fraction, location, node_hovering_callback, node_hovering_callback_data);
        }
        public static int NumSelectedLinks()
        {
            int ret = ImNodesNative.ImNodes_NumSelectedLinks();
            return ret;
        }
        public static int NumSelectedNodes()
        {
            int ret = ImNodesNative.ImNodes_NumSelectedNodes();
            return ret;
        }
        public static void PopAttributeFlag()
        {
            ImNodesNative.ImNodes_PopAttributeFlag();
        }
        public static void PopColorStyle()
        {
            ImNodesNative.ImNodes_PopColorStyle();
        }
        public static void PopStyleVar()
        {
            int count = 1;
            ImNodesNative.ImNodes_PopStyleVar(count);
        }
        public static void PopStyleVar(int count)
        {
            ImNodesNative.ImNodes_PopStyleVar(count);
        }
        public static void PushAttributeFlag(ImNodesAttributeFlags flag)
        {
            ImNodesNative.ImNodes_PushAttributeFlag(flag);
        }
        public static void PushColorStyle(ImNodesCol item, uint color)
        {
            ImNodesNative.ImNodes_PushColorStyle(item, color);
        }
        public static void PushStyleVar(ImNodesStyleVar style_item, float value)
        {
            ImNodesNative.ImNodes_PushStyleVar_Float(style_item, value);
        }
        public static void PushStyleVar(ImNodesStyleVar style_item, Vector2 value)
        {
            ImNodesNative.ImNodes_PushStyleVar_Vec2(style_item, value);
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        public static void SaveCurrentEditorStateToIniFile(ReadOnlySpan<char> file_name)
        {
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_SaveCurrentEditorStateToIniFile(native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
#endif
        public static void SaveCurrentEditorStateToIniFile(string file_name)
        {
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_SaveCurrentEditorStateToIniFile(native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
        public static string SaveCurrentEditorStateToIniString()
        {
            uint* data_size = null;
            byte* ret = ImNodesNative.ImNodes_SaveCurrentEditorStateToIniString(data_size);
            return Util.StringFromPtr(ret);
        }
        public static string SaveCurrentEditorStateToIniString(ref uint data_size)
        {
            fixed (uint* native_data_size = &data_size)
            {
                byte* ret = ImNodesNative.ImNodes_SaveCurrentEditorStateToIniString(native_data_size);
                return Util.StringFromPtr(ret);
            }
        }
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
        public static void SaveEditorStateToIniFile(ImNodesEditorContextPtr editor, ReadOnlySpan<char> file_name)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_SaveEditorStateToIniFile(native_editor, native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
#endif
        public static void SaveEditorStateToIniFile(ImNodesEditorContextPtr editor, string file_name)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            byte* native_file_name;
            int file_name_byteCount = 0;
            if (file_name != null)
            {
                file_name_byteCount = Encoding.UTF8.GetByteCount(file_name);
                if (file_name_byteCount > Util.StackAllocationSizeLimit)
                {
                    native_file_name = Util.Allocate(file_name_byteCount + 1);
                }
                else
                {
                    byte* native_file_name_stackBytes = stackalloc byte[file_name_byteCount + 1];
                    native_file_name = native_file_name_stackBytes;
                }
                int native_file_name_offset = Util.GetUtf8(file_name, native_file_name, file_name_byteCount);
                native_file_name[native_file_name_offset] = 0;
            }
            else { native_file_name = null; }
            ImNodesNative.ImNodes_SaveEditorStateToIniFile(native_editor, native_file_name);
            if (file_name_byteCount > Util.StackAllocationSizeLimit)
            {
                Util.Free(native_file_name);
            }
        }
        public static string SaveEditorStateToIniString(ImNodesEditorContextPtr editor)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            uint* data_size = null;
            byte* ret = ImNodesNative.ImNodes_SaveEditorStateToIniString(native_editor, data_size);
            return Util.StringFromPtr(ret);
        }
        public static string SaveEditorStateToIniString(ImNodesEditorContextPtr editor, ref uint data_size)
        {
            ImNodesEditorContext* native_editor = editor.NativePtr;
            fixed (uint* native_data_size = &data_size)
            {
                byte* ret = ImNodesNative.ImNodes_SaveEditorStateToIniString(native_editor, native_data_size);
                return Util.StringFromPtr(ret);
            }
        }
        public static void SelectLink(int link_id)
        {
            ImNodesNative.ImNodes_SelectLink(link_id);
        }
        public static void SelectNode(int node_id)
        {
            ImNodesNative.ImNodes_SelectNode(node_id);
        }
        public static void SetCurrentContext(ImNodesContextPtr ctx)
        {
            ImNodesContext* native_ctx = ctx.NativePtr;
            ImNodesNative.ImNodes_SetCurrentContext(native_ctx);
        }
        public static void SetImGuiContext(IntPtr ctx)
        {
            ImNodesNative.ImNodes_SetImGuiContext(ctx);
        }
        public static void SetNodeDraggable(int node_id, bool draggable)
        {
            byte native_draggable = draggable ? (byte)1 : (byte)0;
            ImNodesNative.ImNodes_SetNodeDraggable(node_id, native_draggable);
        }
        public static void SetNodeEditorSpacePos(int node_id, Vector2 editor_space_pos)
        {
            ImNodesNative.ImNodes_SetNodeEditorSpacePos(node_id, editor_space_pos);
        }
        public static void SetNodeGridSpacePos(int node_id, Vector2 grid_pos)
        {
            ImNodesNative.ImNodes_SetNodeGridSpacePos(node_id, grid_pos);
        }
        public static void SetNodeScreenSpacePos(int node_id, Vector2 screen_space_pos)
        {
            ImNodesNative.ImNodes_SetNodeScreenSpacePos(node_id, screen_space_pos);
        }
        public static void SnapNodeToGrid(int node_id)
        {
            ImNodesNative.ImNodes_SnapNodeToGrid(node_id);
        }
        public static void StyleColorsClassic()
        {
            ImNodesStyle* dest = null;
            ImNodesNative.ImNodes_StyleColorsClassic(dest);
        }
        public static void StyleColorsClassic(ImNodesStylePtr dest)
        {
            ImNodesStyle* native_dest = dest.NativePtr;
            ImNodesNative.ImNodes_StyleColorsClassic(native_dest);
        }
        public static void StyleColorsDark()
        {
            ImNodesStyle* dest = null;
            ImNodesNative.ImNodes_StyleColorsDark(dest);
        }
        public static void StyleColorsDark(ImNodesStylePtr dest)
        {
            ImNodesStyle* native_dest = dest.NativePtr;
            ImNodesNative.ImNodes_StyleColorsDark(native_dest);
        }
        public static void StyleColorsLight()
        {
            ImNodesStyle* dest = null;
            ImNodesNative.ImNodes_StyleColorsLight(dest);
        }
        public static void StyleColorsLight(ImNodesStylePtr dest)
        {
            ImNodesStyle* native_dest = dest.NativePtr;
            ImNodesNative.ImNodes_StyleColorsLight(native_dest);
        }
    }
}
